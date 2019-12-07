namespace Playground.Blazor.Api
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;

    internal static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = Host
                .CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
            using var host = builder.Build();
            await host.RunAsync();
        }
    }
}