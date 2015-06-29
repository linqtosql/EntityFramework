// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Query.Expressions;
using Microsoft.Data.Entity.Relational.Internal;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Data.Entity.Query.Sql
{
    public class RawSqlQueryGenerator : ISqlQueryGenerator
    {
        private readonly SelectExpression _selectExpression;
        private readonly string _sql;
        private readonly object[] _inputParameters;
        private readonly List<CommandParameter> _commandParameters;

        public RawSqlQueryGenerator(
            [NotNull] SelectExpression selectExpression,
            [NotNull] string sql,
            [NotNull] object[] parameters,
            [NotNull] IRelationalTypeMapper typeMapper)
        {
            Check.NotNull(typeMapper, nameof(typeMapper));
            Check.NotNull(selectExpression, nameof(selectExpression));
            Check.NotNull(sql, nameof(sql));
            Check.NotNull(parameters, nameof(parameters));

            _selectExpression = selectExpression;
            _sql = sql;
            _inputParameters = parameters;
            _commandParameters = new List<CommandParameter>();
            TypeMapper = typeMapper;
        }

        public virtual IRelationalTypeMapper TypeMapper { get; }

        public virtual IReadOnlyList<CommandParameter> Parameters => _commandParameters;

        protected virtual string ParameterPrefix => "@";

        public virtual string GenerateSql(IDictionary<string, object> parameterValues)
        {
            Check.NotNull(parameterValues, nameof(parameterValues));

            _commandParameters.Clear();

            var substitutions = new object[_inputParameters.Length];

            for (var index = 0; index < _inputParameters.Length; index++)
            {
                var parameterName = ParameterPrefix + "p" + index;

                var value = _inputParameters[index];
                _commandParameters.Add(new CommandParameter(parameterName, value, TypeMapper.GetDefaultMapping(value)));

                substitutions[index] = parameterName;
            }

            return string.Format(_sql, substitutions);
        }

        public virtual IRelationalValueBufferFactory CreateValueBufferFactory(
            IRelationalValueBufferFactoryFactory relationalValueBufferFactoryFactory, DbDataReader dataReader)
        {
            Check.NotNull(relationalValueBufferFactoryFactory, nameof(relationalValueBufferFactoryFactory));
            Check.NotNull(dataReader, nameof(dataReader));

            var readerColumns
                = Enumerable
                    .Range(0, dataReader.FieldCount)
                    .Select(i => new
                    {
                        Name = dataReader.GetName(i),
                        Ordinal = i
                    })
                    .ToList();

            var types = new Type[_selectExpression.Projection.Count];
            var indexMap = new int[_selectExpression.Projection.Count];

            for (var i = 0; i < _selectExpression.Projection.Count; i++)
            {
                var aliasExpression = _selectExpression.Projection[i] as AliasExpression;

                if (aliasExpression != null)
                {
                    var columnName
                        = aliasExpression.Alias
                          ?? aliasExpression.TryGetColumnExpression()?.Name;

                    if (columnName != null)
                    {
                        var readerColumn
                            = readerColumns.SingleOrDefault(c =>
                                string.Equals(columnName, c.Name, StringComparison.OrdinalIgnoreCase));

                        if (readerColumn == null)
                        {
                            throw new InvalidOperationException(Strings.FromSqlMissingColumn(columnName));
                        }

                        types[i] = _selectExpression.Projection[i].Type;
                        indexMap[i] = readerColumn.Ordinal;
                    }
                }
            }

            return relationalValueBufferFactoryFactory.Create(types, indexMap);
        }
    }
}
