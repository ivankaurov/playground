using System.IdentityModel.Tokens;
using System.Web.Http;
using AccessTokenValidation.App.Infrastructure;
using IdentityServer3.AccessTokenValidation;
using Microsoft.Owin.Security;
using Owin;

namespace AccessTokenValidation.App
{
    public class Startup
    {

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configuration(IAppBuilder app)
        {
            var cfg = new HttpConfiguration();
            cfg.MapHttpAttributeRoutes();

            app.Use<AggregateAuthorizationMiddleware>(app, new AggregateAuthorizationOptions
            {
                Authorities =
                {
                    "https://login.devintermedia.net/user/",
                    "https://login.devserverdata.net/user/",
                    "https://login.qaintermedia.net/user/",
                    "https://login.qaserverdata.net/user/",
                    "https://uniteapi.devintermedia.net/auth/",
                    "https://uniteapi.qaintermedia.net/auth/",
                    "https://extend-api.devintermedia.net/auth/",
                    "https://extend-api.qaintermedia.net/auth/",
                    "https://elevate-api.devserverdata.net/auth/",
                    "https://elevate-api.qaserverdata.net/auth/",
                },
            });

            app.UseWebApi(cfg);
        }
    }
}
