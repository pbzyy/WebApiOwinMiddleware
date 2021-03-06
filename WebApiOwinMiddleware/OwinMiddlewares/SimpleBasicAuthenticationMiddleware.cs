﻿namespace WebApiOwinMiddleware.OwinMiddlewares
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Owin;

    /// <summary>
    /// Use <see cref="BasicAuthMiddleware" /> instead. The improved version
    /// </summary>
    public class SimpleBasicAuthenticationMiddleware : OwinMiddleware
    {
        public const string AuthMode = "Basic";

        public SimpleBasicAuthenticationMiddleware(OwinMiddleware next) : base(next)
        { }

        public override async Task Invoke(IOwinContext context)
        {
            var response = context.Response;
            var request = context.Request;

            response.OnSendingHeaders(state =>
            {
                var resp = (OwinResponse)state;

                if (resp.StatusCode == 401)
                {
                    resp.Headers.Set("WWW-Authenticate", AuthMode);
                }

                // resp.Headers.Set("X-MyResponse-Header", "Some Value");
                // resp.StatusCode = 403;
                // resp.ReasonPhrase = "Forbidden";
            }, response);

            var authorizationHeaderRaw = context.Request.Headers["Authorization"];

            if (!string.IsNullOrWhiteSpace(authorizationHeaderRaw))
            {
                var authHeader = System.Net.Http.Headers.AuthenticationHeaderValue.Parse(authorizationHeaderRaw);

                if (AuthMode.Equals(authHeader.Scheme, StringComparison.OrdinalIgnoreCase))
                {
                    string parameter = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter));
                    var parts = parameter.Split(':');

                    string userName = parts[0];
                    string password = parts[1];

                    if (userName == password) // Just a dumb check
                    {
                        var claims = new[]
                        {
                            new Claim(ClaimTypes.Name, userName),
                            new Claim(ClaimTypes.Email, "kk@kk.com")
                        };

                        var identity = new ClaimsIdentity(claims, AuthMode);

                        request.User = new ClaimsPrincipal(identity);
                    }
                }
            }

            await this.Next.Invoke(context);
        }

        private void SingIn(IOwinContext context)
        {
            // user login
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, "kk"));
            claims.Add(new Claim(ClaimTypes.Email, "kk@kk.com"));

            var id = new ClaimsIdentity(claims, "cookie");

            context.Authentication.SignIn(id);
        }
    }
}