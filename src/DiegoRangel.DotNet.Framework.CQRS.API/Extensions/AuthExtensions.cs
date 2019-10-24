using System;
using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.API.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Extensions
{
    public static class AuthExtensions
    {
        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration, bool enableWindowsAuth = false)
        {
            var jwtConfigurations = new JwtConfigurations();
            var configurationBuilder = new ConfigureFromConfigurationOptions<JwtConfigurations>(configuration.GetSection("JwtConfigurations"));
            configurationBuilder.Configure(jwtConfigurations);
            services.AddSingleton(jwtConfigurations);

            var builder = services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = enableWindowsAuth ? IISDefaults.AuthenticationScheme : JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });

            builder.AddJwtBearer(cfg =>
            {
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true, //validate the server that created that token
                    ValidIssuer = jwtConfigurations.Issuer,

                    ValidateAudience = true, //ensure that the recipient of the token is authorized to receive it
                    ValidAudience = jwtConfigurations.Audience,

                    ValidateIssuerSigningKey = true, //verify that the key used to sign the incoming token is part of a list of trusted keys
                    IssuerSigningKey = JwtSecurityKey.Create(jwtConfigurations.Key),

                    ValidateLifetime = true, //check that the token is not expired and that the signing key of the issuer is valid 
                    ClockSkew = TimeSpan.Zero
                };

                cfg.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
                        logger.LogError("Authentication failed.", context.Exception);
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
                        logger.LogError("OnChallenge error", context.Error, context.ErrorDescription);
                        return Task.CompletedTask;
                    },
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        if (string.IsNullOrEmpty(context.Token) && !string.IsNullOrEmpty(accessToken))
                            context.Token = accessToken;

                        return Task.CompletedTask;
                    }
                };
            });
        }

        public static void AddWindowsAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(IISDefaults.AuthenticationScheme);
        }
    }
}