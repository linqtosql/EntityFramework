// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata.Internal;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Data.Entity.Metadata
{
    public class Key : Annotatable, IKey
    {
        public Key([NotNull] IReadOnlyList<Property> properties)
        {
            Check.NotEmpty(properties, nameof(properties));
            Check.HasNoNulls(properties, nameof(properties));
            MetadataHelper.CheckSameEntityType(properties, "properties");

            Properties = properties;
        }

        public virtual IReadOnlyList<Property> Properties { get; }

        public virtual EntityType EntityType => Properties[0].EntityType;

        IReadOnlyList<IProperty> IKey.Properties => Properties;

        IEntityType IKey.EntityType => EntityType;
    }
}
