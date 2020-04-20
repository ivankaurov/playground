namespace Playground.Blazor.Wasm.Application
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using Playground.Blazor.Core;
    using Playground.Blazor.Core.Calc;
    using Playground.Blazor.Core.Clock;
    using Playground.Blazor.Core.Utils;

    internal static class Program
    {
        public static async Task Main(string[] args)
        {
            await WebAssemblyHostBuilder
                .CreateDefault(args)
                .ConfigureServices()
                .ConfigureComponents()
                .Build()
                .StartHostedServices()
                .RunAsync();
        }

        private static WebAssemblyHostBuilder ConfigureServices(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddWeatherForecastService()
                .AddHttpWeatherForecastService(opt => opt.BaseUri = "http://localhost:5000/")
                .AddCalcService()
                .AddHttpCalcService(opt => opt.BaseUri = "http://localhost:5000/")
                .AddClockService();
            return builder;
        }

        private static WebAssemblyHostBuilder ConfigureComponents(this WebAssemblyHostBuilder builder)
        {
            builder.RootComponents.Add<App>("app");
            return builder;
        }

        private static WebAssemblyHost StartHostedServices(this WebAssemblyHost webAssemblyHost)
        {
            var hostedServices = webAssemblyHost.Services.GetServices<IHostedService>();
            Task.WhenAll(hostedServices.Select(s => s.StartAsync(CancellationToken.None))).Forget();
            return webAssemblyHost;
        }
    }
}
