namespace Playground.Blazor.Core
{
    using System;

    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWeatherForecastService(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSingleton<IWeatherForecastService, WeatherForecastService>();
        }

        public static IServiceCollection AddHttpWeatherForecastService(
            this IServiceCollection serviceCollection,
            Action<HttpWeatherServiceConfiguration>? configureOptions = null)
        {
            serviceCollection.AddHttpClient<IHttpWeatherForecastService, HttpWeatherService>();

            if (configureOptions != null)
            {
                serviceCollection.AddOptions<HttpWeatherServiceConfiguration>().Configure(configureOptions);
            }

            return serviceCollection;
        }
    }
}