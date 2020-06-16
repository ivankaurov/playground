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
        private readonly IReadOnlyDictionary<string, ApiResource> resourcesByScope;
        private readonly IReadOnlyDictionary<string, ApiResource> resourcesByName;

        public ConfigResourceStore(IOptions<SettingsConfig> config)
        {
            var resourcesByScope = new Dictionary<string, ApiResource>(StringComparer.Ordinal);
            this.resourcesByName = config.Value.Resources.ToDictionary(
                r => r.Id,
                r =>
                {
                    var apiResource = new ApiResource(r.Id, r.DisplayName);
                    foreach (var scope in apiResource.Scopes)
                    {
                        resourcesByScope.Add(scope.Name, apiResource);
                    }

                    return apiResource;
                },
                StringComparer.Ordinal);

            this.resourcesByScope = resourcesByScope;
        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            return Task.FromResult<IEnumerable<IdentityResource>>(Array.Empty<IdentityResource>());
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            return Task.FromResult(scopeNames.Select(scope => this.resourcesByScope.TryGetValue(scope, out var result) ? result : null).Where(res => res != null).Select(_ => _!));
        }

        public Task<ApiResource?> FindApiResourceAsync(string name)
        {
            return Task.FromResult(this.resourcesByName.TryGetValue(name, out var res) ? res : null);
        }

        public Task<Resources> GetAllResourcesAsync()
        {
            return Task.FromResult(new Resources(Array.Empty<IdentityResource>(), this.resourcesByName.Values));
        }
    }
}