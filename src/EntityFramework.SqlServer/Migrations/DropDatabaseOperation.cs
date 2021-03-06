// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.Data.Entity.Relational.Migrations.Operations;

namespace Microsoft.Data.Entity.SqlServer.Migrations
{
    public class DropDatabaseOperation : MigrationOperation
    {
        public virtual string Name { get; [param: NotNull] set; }
    }
}
