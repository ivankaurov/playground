namespace Playground.IDP.Application.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using IdentityServer4.Models;
    using IdentityServer4.Stores;
    using Microsoft.Extensions.Options;

    internal sealed class ConfigClientStore : IClientStore
    {
        private readonly ICollection<ClientConfig> clients;
        private readonly IResourceStore resourceStore;

        public ConfigClientStore(IOptions<SettingsConfig> options, IResourceStore resourceStore)
        {
            this.clients = options.Value.Clients;
            this.resourceStore = resourceStore;
        }

        public async Task<Client?> FindClientByIdAsync(string clientId)
        {
            var scopes = (await this.resourceStore.GetAllEnabledResourcesAsync()).ApiResources.SelectMany(s => s.Scopes.Select(s => s.Name)).ToList();
            var cfg = this.clients.FirstOrDefault(c => c.Name.Equals(clientId, StringComparison.Ordinal));
            return cfg == null ? null : new Client { ClientId = clientId, ClientName = cfg.DisplayName, Enabled = true, AllowedScopes = scopes, };
        }
    }
}
