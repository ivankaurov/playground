namespace Playground.Blazor.Core.Calc
{
    using System;

    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCalcService(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSingleton<ICalcService, CalcService>();
        }

        public static IServiceCollection AddHttpCalcServiceWithHttpClient(
            this IServiceCollection serviceCollection,
            Action<HttpCalcServiceConfiguration>? configureOptions = null)
        {
            serviceCollection.ConfigureHttpCalcService(configureOptions)
                .AddHttpClient<IHttpCalcService, HttpCalcService>();

            return serviceCollection;
        }

        public static IServiceCollection AddHttpCalcService(
            this IServiceCollection serviceCollection,
            Action<HttpCalcServiceConfiguration>? configureOptions = null)
        {
            return serviceCollection.AddTransient<IHttpCalcService, HttpCalcService>()
                .ConfigureHttpCalcService(configureOptions);
        }

        private static IServiceCollection ConfigureHttpCalcService(
            this IServiceCollection serviceCollection,
            Action<HttpCalcServiceConfiguration>? configureOptions)
        {
            if (configureOptions != null)
            {
                serviceCollection.AddOptions<HttpCalcServiceConfiguration>().Configure(configureOptions);
            }

            return serviceCollection;
        }
    }
}