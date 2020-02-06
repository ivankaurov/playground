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

        public static IServiceCollection AddHttpCalcService(
            this IServiceCollection serviceCollection,
            Action<HttpCalcServiceConfiguration>? configureOptions = null)
        {
            serviceCollection.AddHttpClient<IHttpCalcService, HttpCalcService>();
            if (configureOptions != null)
            {
                serviceCollection.AddOptions<HttpCalcServiceConfiguration>().Configure(configureOptions);
            }

            return serviceCollection;
        }
    }
}