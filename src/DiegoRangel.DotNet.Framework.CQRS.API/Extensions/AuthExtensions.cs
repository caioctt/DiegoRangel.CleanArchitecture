using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Extensions.DependencyInjection;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Extensions
{
    public static class AuthExtensions
    {
        public static void AddJwtAuthentication(this IServiceCollection services, Action<AuthenticationOptions> authOptionsBuilder, Action<JwtBearerOptions> jwtOptionsBuilder)
        {
            services
                .AddAuthentication(authOptionsBuilder)
                .AddJwtBearer(jwtOptionsBuilder);
        }

        public static void AddWindowsAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(IISDefaults.AuthenticationScheme);
        }
    }
}