// -----------------------------------------------------------------------
// <copyright file="AggregateAuthorizationMiddleware.cs" company="Intermedia">
//   Copyright © Intermedia.net, Inc. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics;
using System.Linq;

namespace AccessTokenValidation.App.Infrastructure
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens;
    using System.Threading.Tasks;
    using IdentityServer3.AccessTokenValidation;
    using Microsoft.Owin;
    using Microsoft.Owin.Builder;
    using Microsoft.Owin.Security;
    using Owin;

    public class AggregateAuthorizationMiddleware
    {
        private const string BearerPrefix = "Bearer ";
        private const int BearerPrefixLength = 7;

        private readonly Func<IDictionary<string, object>, Task> next;
        private readonly IAppBuilder app;
        private readonly AggregateAuthorizationOptions options;
        private readonly HashSet<string> validIssuers;

        private readonly ConcurrentDictionary<string, Func<IDictionary<string, object>, Task>> middlewareCache =
            new ConcurrentDictionary<string, Func<IDictionary<string, object>, Task>>(StringComparer.OrdinalIgnoreCase);

        public AggregateAuthorizationMiddleware(
            Func<IDictionary<string, object>, Task> next,
            IAppBuilder app,
            AggregateAuthorizationOptions options)
        {
            this.next = next;
            this.app = app;
            this.options = options;
            this.validIssuers = new HashSet<string>(this.options.Authorities.Select(a => a.TrimEnd('/')), StringComparer.OrdinalIgnoreCase);
        }

        public Task Invoke(IDictionary<string, object> environment)
        {
            if (!this.TryGetToken(environment, out var token))
            {
                return this.next(environment);
            }

            if (!this.validIssuers.Contains(token.Issuer))
            {
                // Invalid issuer
                return this.next(environment);
            }

            var nextAction =
                this.middlewareCache.GetOrAdd(token.Issuer, issuer => this.CreateBuilderForAuthority(issuer).Build());
            return nextAction(environment);
        }

        private bool TryGetToken(IDictionary<string, object> environment, out JwtSecurityToken token)
        {
            var owinRequest = new OwinRequest(environment);
            var tokenString = owinRequest.Headers.Get("Authorization");
            if (tokenString == null || tokenString.Length <= BearerPrefixLength || !tokenString.StartsWith(BearerPrefix, StringComparison.Ordinal))
            {
                token = null;
                return false;
            }

            try
            {
                tokenString = tokenString.Substring(BearerPrefixLength).Trim();
                var sw = Stopwatch.StartNew();
                token = new JwtSecurityToken(tokenString);
                sw.Stop();

                sw.Restart();
                for (var i = 0; i < 100000; i++)
                {
                    token = new JwtSecurityToken(tokenString);
                }
                sw.Stop();

                Console.WriteLine($"Average: {(sw.ElapsedMilliseconds / 100000.0):0.000} ms");

                return true;
            }
            catch (Exception ex)
            {
                token = null;
                return false;
            }
        }

        private IAppBuilder CreateBuilderForAuthority(string issuer)
        {
            var localBuilder = this.app.New();
            localBuilder.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                ValidationMode = ValidationMode.Local,
                Authority = issuer,
                RequiredScopes = this.options.RequiredScopes,
                DelayLoadMetadata = this.options.DelayLoadMetadata,
            });
            localBuilder.Run(context => this.next(context.Environment));
            return localBuilder;
        }
    }
}