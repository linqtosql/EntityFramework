// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;

namespace Microsoft.Data.Entity.Update
{
    public interface IModificationCommandBatchFactory
    {
        ModificationCommandBatch Create(
            [NotNull] IDbContextOptions options,
            [NotNull] IRelationalMetadataExtensionProvider metadataExtensionProvider);

        bool AddCommand(
            [NotNull] ModificationCommandBatch modificationCommandBatch,
            [NotNull] ModificationCommand modificationCommand);
    }
}
