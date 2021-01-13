using IdentityServer4;

namespace Playground.IDP.Application.Config.Clients
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
        private static readonly string[] StandardScopes = new[]
        {
            IdentityServerConstants.StandardScopes.OpenId,
            IdentityServerConstants.StandardScopes.Profile,
        };
        private readonly ICollection<ClientConfig> clients;
        private readonly IResourceStore resourceStore;

        public ConfigClientStore(IOptions<SettingsConfig> options, IResourceStore resourceStore)
        {
            this.clients = options.Value.Clients;
            this.resourceStore = resourceStore;
        }

        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            var scopes = (await this.resourceStore.GetAllEnabledResourcesAsync()).ApiResources.SelectMany(s => s.Scopes)
                .Concat(StandardScopes);
            var cfg = this.clients.FirstOrDefault(c => c.Id.Equals(clientId, StringComparison.Ordinal));

            if (cfg == null)
            {
                return null;
            }

            var client = new Client
            {
                ClientId = clientId,
                ClientName = cfg.DisplayName,
                Enabled = true,
                AllowedScopes = (await this.resourceStore.GetAllEnabledResourcesAsync()).ApiResources.SelectMany(s => s.Scopes).Concat(StandardScopes).ToList(),
                RequireConsent = true,
                AllowedGrantTypes = GrantTypes.Code,
                AllowOfflineAccess = true,
            };

            if (!string.IsNullOrEmpty(cfg.Secret))
            {
                client.ClientSecrets.Add(new Secret(cfg.Secret.Sha256()));
            }

            if (!string.IsNullOrEmpty(cfg.RedirectUri))
            {
                client.RedirectUris.Add(cfg.RedirectUri);
            }

            return client;
        }
    }
}
