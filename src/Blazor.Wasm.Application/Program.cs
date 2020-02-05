namespace Playground.Blazor.Wasm.Application
{
    using Microsoft.AspNetCore.Blazor.Hosting;
    using Microsoft.Extensions.DependencyInjection;

    using Playground.Blazor.Core;
    using Playground.Blazor.Core.Calc;
    using Playground.Blazor.Core.Clock;

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebAssemblyHostBuilder CreateHostBuilder(string[] args) =>
            BlazorWebAssemblyHost.CreateDefaultBuilder().ConfigureServices(ConfigureServices)
                .UseBlazorStartup<Startup>();

        private static void ConfigureServices(WebAssemblyHostBuilderContext context, IServiceCollection serviceCollection)
        {
            serviceCollection.AddWeatherForecastService()
                .AddHttpWeatherForecastService(opt => opt.BaseUri = "http://localhost:5000/").AddCalcService()
                .AddHttpCalcService(opt => opt.BaseUri = "http://localhost:5000/").AddClockService();
        }
    }
}
