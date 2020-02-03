namespace Playground.Blazor.Core
{
    using System;
    using System.Net.Http.Headers;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

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
            serviceCollection.AddTransient<IHttpWeatherForecastService, HttpWeatherService>();
            if (configureOptions != null)
            {
                serviceCollection.ConfigureHttpWeatherServiceOptions(configureOptions);
            }

            return serviceCollection;
        }

        public static IServiceCollection AddHttpWeatherForecastServiceWithHttpClient(
            this IServiceCollection serviceCollection,
            Action<HttpWeatherServiceConfiguration>? configureOptions = null)
        {
            serviceCollection.AddHttpClient<IHttpWeatherForecastService, HttpWeatherService>();
            if (configureOptions != null)
            {
                serviceCollection.ConfigureHttpWeatherServiceOptions(configureOptions);
            }

            return serviceCollection;
        }

        private static IServiceCollection ConfigureHttpWeatherServiceOptions(
            this IServiceCollection serviceCollection,
            Action<HttpWeatherServiceConfiguration> configureOptions)
        {
            serviceCollection.AddOptions<HttpWeatherServiceConfiguration>().Configure(configureOptions);
            return serviceCollection;
        }
    }
}