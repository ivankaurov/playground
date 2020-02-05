namespace Playground.Blazor.Core.Events
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventBus(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddSingleton<IEventBus, EventBus>();
            return serviceCollection;
        }
    }
}