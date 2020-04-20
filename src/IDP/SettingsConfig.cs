namespace Playground.IDP.Application
{
    using System.Collections.Generic;
    using Playground.IDP.Application.Clients;
    using Playground.IDP.Application.Resources;

    public sealed class SettingsConfig
    {
        public IList<ResourceConfig> Resources { get; } = new List<ResourceConfig>();

        public IList<ClientConfig> Clients { get; } = new List<ClientConfig>();
    }
}