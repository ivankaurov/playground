namespace Playground.Blazor.Core.Clock
{
    using System;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using Playground.Blazor.Core.Events;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddClockService(
            this IServiceCollection serviceCollection,
            Action<ClockServiceConfiguration>? configureClock = null)
        {
            if (configureClock != null)
            {
                serviceCollection.AddOptions<ClockServiceConfiguration>().Configure(configureClock);
            }

            return serviceCollection.AddEventBus().AddSingleton<IHostedService, ClockService>();
        }
    }
}