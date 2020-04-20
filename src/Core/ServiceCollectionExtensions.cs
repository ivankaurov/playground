namespace Playground.Blazor.Core
{
    using System;

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
            serviceCollection.AddHttpClient(HttpWeatherService.HttpClientName).ConfigureHttpClient(
                (sp, client) =>
                    {
                        var options = sp.GetRequiredService<IOptions<HttpWeatherServiceConfiguration>>().Value;
                        client.BaseAddress = new Uri(options.BaseUri);
                    });

            serviceCollection.AddSingleton<IHttpWeatherForecastService, HttpWeatherService>();

            if (configureOptions != null)
            {
                serviceCollection.AddOptions<HttpWeatherServiceConfiguration>().Configure(configureOptions);
            }

            return serviceCollection;
        }
    }
}