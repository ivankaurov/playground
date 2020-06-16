// -----------------------------------------------------------------------
// <copyright file="IValidatableConfig.cs" company="Intermedia">
//   Copyright © Intermedia.net, Inc. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Playground.IDP.Application.Config
{
    using System.Collections.Generic;

    internal interface IValidatableConfig
    {
        IEnumerable<string> Validate();
    }
}