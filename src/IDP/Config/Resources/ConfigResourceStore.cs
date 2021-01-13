namespace Playground.IDP.Application.Config.Resources
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using IdentityServer4.Models;
    using IdentityServer4.Stores;
    using Microsoft.Extensions.Options;

    internal sealed class ConfigResourceStore : IResourceStore
    {
        private static readonly IdentityResource[] PreDefined = { new IdentityResources.OpenId(), new IdentityResources.Profile(), };

        private readonly IDictionary<ApiScope, ApiResource> resourcesByScope = new Dictionary<ApiScope, ApiResource>();

        private readonly IDictionary<string, Resource> resourcesByName = new Dictionary<string, Resource>(StringComparer.Ordinal);

        private readonly IDictionary<string, ApiScope>
            scopes = new Dictionary<string, ApiScope>(StringComparer.Ordinal);

        public ConfigResourceStore(IOptions<SettingsConfig> config)
        {
            foreach (var identityResource in PreDefined)
            {
                this.resourcesByName.Add(identityResource.Name, identityResource);
            }

            foreach (var r in config.Value.Resources)
            {
                var apiResource = new ApiResource(r.Id, r.DisplayName){ Enabled = true, };
                foreach (var scopeName in apiResource.Scopes)
                {
                    var scope = new ApiScope(scopeName);
                    this.scopes.Add(scopeName, scope);
                    this.resourcesByScope.Add(scope, apiResource);
                }

                this.resourcesByName.Add(r.Id, apiResource);
            }
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            var scopes = this.scopes.Find(scopeNames);
            var resources = this.resourcesByScope.Find(scopes);
            return Task.FromResult(resources);
        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            var result = this.resourcesByName.Find(scopeNames).OfType<IdentityResource>();
            return Task.FromResult(result);
        }

        public Task<IEnumerable<ApiScope>> FindApiScopesByNameAsync(IEnumerable<string> scopeNames)
        {
            return Task.FromResult(this.scopes.Find(scopeNames));
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByNameAsync(IEnumerable<string> apiResourceNames)
        {
            return Task.FromResult(this.resourcesByName.Find(apiResourceNames).OfType<ApiResource>());
        }

        public Task<Resources> GetAllResourcesAsync()
        {
            return Task.FromResult(new Resources(Array.Empty<IdentityResource>(), this.resourcesByScope.Values, this.resourcesByScope.Keys));
        }
    }
}