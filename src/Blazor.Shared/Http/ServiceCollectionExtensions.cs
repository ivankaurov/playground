namespace Playground.Blazor.Shared.Http
{
    using System.Linq;
    using System.Net.Http;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlazorWasmHttpClientFactory(this IServiceCollection serviceCollection)
        {
            var httpClientFactoryDescriptors = serviceCollection.Where(
                s => s.ServiceType == typeof(IHttpClientFactory)
                     && s.ImplementationType != typeof(BlazorWasmHttpClientFactory)).ToList();

            foreach (var descriptor in httpClientFactoryDescriptors)
            {
                serviceCollection.Remove(descriptor);
            }

            serviceCollection.TryAddSingleton<IHttpClientFactory, BlazorWasmHttpClientFactory>();
            return serviceCollection;
        }
    }
}