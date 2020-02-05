namespace Playground.Blazor.Wasm.Application
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Components.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using Playground.Blazor.Core.Utils;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");

            var hostedServices = app.Services.GetServices<IHostedService>();
            Task.WhenAll(hostedServices.Select(s => s.StartAsync(CancellationToken.None))).Forget();
        }
    }
}
