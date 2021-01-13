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
        private readonly IReadOnlyDictionary<ApiScope, ApiResource> resourcesByScope;
        private readonly IReadOnlyDictionary<string, ApiResource> resourcesByName;
        private readonly IReadOnlyDictionary<string, ApiScope> scopes;

        public ConfigResourceStore(IOptions<SettingsConfig> config)
        {
            var scopes = new Dictionary<string, ApiScope>(StringComparer.Ordinal);
            var resourcesByScope = new Dictionary<ApiScope, ApiResource>();
            this.resourcesByName = config.Value.Resources.ToDictionary(
                r => r.Id,
                r =>
                {
                    var apiResource = new ApiResource(r.Id, r.DisplayName);
                    foreach (var scopeName in apiResource.Scopes)
                    {
                        var scope = new ApiScope(scopeName);
                        scopes.Add(scopeName, scope);
                        resourcesByScope.Add(scope, apiResource);
                    }

                    return apiResource;
                },
                StringComparer.Ordinal);

            this.resourcesByScope = resourcesByScope;
            this.scopes = scopes;
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            var scopes = this.scopes.Find(scopeNames);
            var resources = this.resourcesByScope.Find(scopes);
            return Task.FromResult(resources);
        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            return Task.FromResult<IEnumerable<IdentityResource>>(Array.Empty<IdentityResource>());
        }

        public Task<IEnumerable<ApiScope>> FindApiScopesByNameAsync(IEnumerable<string> scopeNames)
        {
            return Task.FromResult(this.scopes.Find(scopeNames));
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByNameAsync(IEnumerable<string> apiResourceNames)
        {
            return Task.FromResult(this.resourcesByName.Find(apiResourceNames));
        }

        public Task<Resources> GetAllResourcesAsync()
        {
            return Task.FromResult(new Resources(Array.Empty<IdentityResource>(), this.resourcesByName.Values, this.resourcesByScope.Keys));
        }
    }
}