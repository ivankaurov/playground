namespace Playground.Blazor.Wasm.Application
{
    using Microsoft.AspNetCore.Blazor.Hosting;

    using Playground.Blazor.Core;

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebAssemblyHostBuilder CreateHostBuilder(string[] args) =>
            BlazorWebAssemblyHost.CreateDefaultBuilder().ConfigureServices(
                s => s.AddHttpWeatherForecastService(opt => opt.BaseUri = "http://localhost:5000/")
                    .AddWeatherForecastService()).UseBlazorStartup<Startup>();
    }
}
