// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Builders;
using Microsoft.Data.Entity.SqlServer.Metadata;
using Microsoft.Data.Entity.Utilities;

// ReSharper disable once CheckNamespace

namespace Microsoft.Data.Entity
{
    public static class SqlServerPropertyBuilderExtensions
    {
        public static PropertyBuilder HasSqlServerColumnName(
            [NotNull] this PropertyBuilder propertyBuilder,
            [CanBeNull] string name)
        {
            Check.NotNull(propertyBuilder, nameof(propertyBuilder));
            Check.NullButNotEmpty(name, nameof(name));

            propertyBuilder.Metadata.SqlServer().Column = name;

            return propertyBuilder;
        }

        public static PropertyBuilder<TProperty> HasSqlServerColumnName<TProperty>(
            [NotNull] this PropertyBuilder<TProperty> propertyBuilder,
            [CanBeNull] string name)
            => (PropertyBuilder<TProperty>)HasSqlServerColumnName((PropertyBuilder)propertyBuilder, name);

        public static PropertyBuilder HasSqlServerColumnType(
            [NotNull] this PropertyBuilder propertyBuilder,
            [CanBeNull] string typeName)
        {
            Check.NotNull(propertyBuilder, nameof(propertyBuilder));
            Check.NullButNotEmpty(typeName, nameof(typeName));

            propertyBuilder.Metadata.SqlServer().ColumnType = typeName;

            return propertyBuilder;
        }

        public static PropertyBuilder<TProperty> HasSqlServerColumnType<TProperty>(
            [NotNull] this PropertyBuilder<TProperty> propertyBuilder,
            [CanBeNull] string typeName)
            => (PropertyBuilder<TProperty>)HasSqlServerColumnType((PropertyBuilder)propertyBuilder, typeName);

        public static PropertyBuilder SqlServerDefaultValueSql(
            [NotNull] this PropertyBuilder propertyBuilder,
            [CanBeNull] string sql)
        {
            Check.NotNull(propertyBuilder, nameof(propertyBuilder));
            Check.NullButNotEmpty(sql, nameof(sql));

            propertyBuilder.Metadata.SqlServer().DefaultValueSql = sql;

            return propertyBuilder;
        }

        public static PropertyBuilder<TProperty> SqlServerDefaultValueSql<TProperty>(
            [NotNull] this PropertyBuilder<TProperty> propertyBuilder,
            [CanBeNull] string sql)
            => (PropertyBuilder<TProperty>)SqlServerDefaultValueSql((PropertyBuilder)propertyBuilder, sql);

        public static PropertyBuilder SqlServerDefaultValue(
            [NotNull] this PropertyBuilder propertyBuilder,
            [CanBeNull] object value)
        {
            Check.NotNull(propertyBuilder, nameof(propertyBuilder));

            propertyBuilder.Metadata.SqlServer().DefaultValue = value;

            return propertyBuilder;
        }

        public static PropertyBuilder<TProperty> SqlServerDefaultValue<TProperty>(
            [NotNull] this PropertyBuilder<TProperty> propertyBuilder,
            [CanBeNull] object value)
            => (PropertyBuilder<TProperty>)SqlServerDefaultValue((PropertyBuilder)propertyBuilder, value);

        public static PropertyBuilder SqlServerComputedExpression(
            [NotNull] this PropertyBuilder propertyBuilder,
            [CanBeNull] string sql)
        {
            Check.NotNull(propertyBuilder, nameof(propertyBuilder));
            Check.NullButNotEmpty(sql, nameof(sql));

            propertyBuilder.Metadata.SqlServer().ComputedExpression = sql;

            return propertyBuilder;
        }

        public static PropertyBuilder<TProperty> SqlServerComputedExpression<TProperty>(
            [NotNull] this PropertyBuilder<TProperty> propertyBuilder,
            [CanBeNull] string sql)
            => (PropertyBuilder<TProperty>)SqlServerComputedExpression((PropertyBuilder)propertyBuilder, sql);

        public static PropertyBuilder UseSqlServerSequenceHiLo(
            [NotNull] this PropertyBuilder propertyBuilder,
            [CanBeNull] string name = null,
            [CanBeNull] string schema = null)
        {
            Check.NotNull(propertyBuilder, nameof(propertyBuilder));
            Check.NullButNotEmpty(name, nameof(name));
            Check.NullButNotEmpty(schema, nameof(schema));

            var property = propertyBuilder.Metadata;
            var sequence = property.EntityType.Model.SqlServer().GetOrAddSequence(name, schema);

            property.SqlServer().IdentityStrategy = SqlServerIdentityStrategy.SequenceHiLo;
            property.StoreGeneratedPattern = StoreGeneratedPattern.Identity;
            property.SqlServer().SequenceName = sequence.Name;
            property.SqlServer().SequenceSchema = sequence.Schema;

            return propertyBuilder;
        }

        public static PropertyBuilder<TProperty> UseSqlServerSequenceHiLo<TProperty>(
            [NotNull] this PropertyBuilder<TProperty> propertyBuilder,
            [CanBeNull] string name = null,
            [CanBeNull] string schema = null)
            => (PropertyBuilder<TProperty>)UseSqlServerSequenceHiLo((PropertyBuilder)propertyBuilder, name, schema);

        public static PropertyBuilder UseSqlServerIdentityColumn(
            [NotNull] this PropertyBuilder propertyBuilder)
        {
            Check.NotNull(propertyBuilder, nameof(propertyBuilder));

            var property = propertyBuilder.Metadata;

            property.SqlServer().IdentityStrategy = SqlServerIdentityStrategy.IdentityColumn;
            property.StoreGeneratedPattern = StoreGeneratedPattern.Identity;
            property.SqlServer().SequenceName = null;
            property.SqlServer().SequenceSchema = null;

            return propertyBuilder;
        }

        public static PropertyBuilder<TProperty> UseSqlServerIdentityColumn<TProperty>(
            [NotNull] this PropertyBuilder<TProperty> propertyBuilder)
            => (PropertyBuilder<TProperty>)UseSqlServerIdentityColumn((PropertyBuilder)propertyBuilder);
    }
}
