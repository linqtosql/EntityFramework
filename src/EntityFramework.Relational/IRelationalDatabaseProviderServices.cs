// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Data.Entity.Relational.Metadata;
using Microsoft.Data.Entity.Relational.Migrations.History;
using Microsoft.Data.Entity.Relational.Migrations.Infrastructure;
using Microsoft.Data.Entity.Relational.Migrations.Sql;
using Microsoft.Data.Entity.Relational.Query.Methods;
using Microsoft.Data.Entity.Relational.Update;
using Microsoft.Data.Entity.Storage;

namespace Microsoft.Data.Entity.Relational
{
    public interface IRelationalDatabaseProviderServices : IDatabaseProviderServices
    {
        IMigrationAnnotationProvider MigrationAnnotationProvider { get; }
        IHistoryRepository HistoryRepository { get; }
        IMigrationSqlGenerator MigrationSqlGenerator { get; }
        IRelationalConnection RelationalConnection { get; }
        IRelationalTypeMapper TypeMapper { get; }
        IUpdateSqlGenerator UpdateSqlGenerator { get; }
        IModificationCommandBatchFactory ModificationCommandBatchFactory { get; }
        ICommandBatchPreparer CommandBatchPreparer { get; }
        IBatchExecutor BatchExecutor { get; }
        IRelationalValueBufferFactoryFactory ValueBufferFactoryFactory { get; }
        IRelationalDatabaseCreator RelationalDatabaseCreator { get; }
        IRelationalMetadataExtensionProvider MetadataExtensionProvider { get; }
        ISqlStatementExecutor SqlStatementExecutor { get; }
        IMethodCallTranslator CompositeMethodCallTranslator { get; }
        IMemberTranslator CompositeMemberTranslator { get; }
        IParameterNameGeneratorFactory ParameterNameGeneratorFactory { get; }
    }
}
