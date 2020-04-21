namespace Playground.IDP.Application.Config
{
    using System.Collections.Generic;

    using Playground.IDP.Application.Config.Clients;
    using Playground.IDP.Application.Config.Resources;

    public sealed class SettingsConfig
    {
        public IList<ResourceConfig> Resources { get; } = new List<ResourceConfig>();

        public IList<ClientConfig> Clients { get; } = new List<ClientConfig>();
    }
}