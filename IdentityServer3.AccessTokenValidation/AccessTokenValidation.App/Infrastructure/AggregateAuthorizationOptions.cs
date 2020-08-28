// -----------------------------------------------------------------------
// <copyright file="AggregateAuthorizationOptions.cs" company="Intermedia">
//   Copyright © Intermedia.net, Inc. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace AccessTokenValidation.App.Infrastructure
{
    using System.Collections.Generic;

    public sealed class AggregateAuthorizationOptions
    {
        public ICollection<string> Authorities { get; } = new List<string>();

        public ICollection<string> RequiredScopes { get; } = new List<string>();

        public bool DelayLoadMetadata { get; set; }
    }
}