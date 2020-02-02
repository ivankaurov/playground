namespace Playground.Blazor.Core
{
    using System;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCoreServices(
            this IServiceCollection serviceCollection,
            Action<HttpWeatherServiceConfiguration>? configureOptions = null)
        {
            serviceCollection.AddSingleton<IWeatherForecastService, WeatherForecastService>();

            serviceCollection.AddHttpClient<IHttpWeatherForecastService, HttpWeatherService>(
                (sp, client) => client.BaseAddress = new Uri(
                                    sp.GetRequiredService<IOptions<HttpWeatherServiceConfiguration>>().Value.BaseUri));

            var optionsBuilder = serviceCollection.AddOptions<HttpWeatherServiceConfiguration>();
            if (configureOptions != null)
            {
                optionsBuilder.Configure(configureOptions);
            }

            return serviceCollection;
        }
    }
}