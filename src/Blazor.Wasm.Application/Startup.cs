namespace Playground.Blazor.Wasm.Application
{
    using System;

    using Microsoft.AspNetCore.Components.Builder;
    using Microsoft.Extensions.DependencyInjection;

    using Playground.Blazor.Core;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCoreServices(opt => opt.BaseUri = "http://localhost:5000/");
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
