namespace Playground.IDP.Application
{
    using IdentityServer4.Stores;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Options;
    using Playground.IDP.Application.Config;
    using Playground.IDP.Application.Config.Clients;
    using Playground.IDP.Application.Config.Resources;

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
            services
                .Configure<SettingsConfig>(this.configuration.GetSection("idsrv"))
                .AddSingleton<IValidateOptions<SettingsConfig>, OptionsValidator<SettingsConfig>>();

            services.AddSingleton<IResourceStore, ConfigResourceStore>().AddSingleton<IClientStore, ConfigClientStore>();

            services.AddIdentityServer().AddDeveloperSigningCredential(persistKey: false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();
        }
    }
}
