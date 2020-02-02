namespace Playground.Blazor.Api
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics.HealthChecks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using Playground.Blazor.Core;

    internal sealed class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddControllersAsServices();

            services.AddHealthChecks();
            services.AddCoreServices();
            services.AddCors(c => c.AddDefaultPolicy(
                p =>
                    {
                        p.AllowCredentials();
                        p.SetIsOriginAllowed(_ => true);
                        p.AllowAnyMethod();
                        p.AllowAnyHeader();
                    }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseCors()
                .UseHealthChecks("/health", new HealthCheckOptions { AllowCachingResponses = false, })
                .UseRouting()
                .UseEndpoints(
                    endpoints =>
                        {
                            endpoints.MapGet(
                                "/",
                                async context => { await context.Response.WriteAsync("Hello World!"); });
                            endpoints.MapControllers();
                        });
        }
    }
}