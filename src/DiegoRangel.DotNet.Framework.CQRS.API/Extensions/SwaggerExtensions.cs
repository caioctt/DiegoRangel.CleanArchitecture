using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Extensions
{
    public static class SwaggerExtensions
    {
        public static void AddSwaggerDocumentation(this IServiceCollection services, Action<SwaggerSettings> swaggerSettingsBuilder)
        {
            var swaggerSettings = new SwaggerSettings();
            swaggerSettingsBuilder(swaggerSettings);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = swaggerSettings.ApiTitle,
                    Description = swaggerSettings.ApiDescription,
                    Contact = new OpenApiContact { Name = swaggerSettings.ApiContactInfo }
                });

                if (!swaggerSettings.SecureWithUseJwtAuth) return;

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app, Action<SwaggerUiSettings> swaggerUiSettingsBuilder)
        {
            var swaggerUiSettings = new SwaggerUiSettings();
            swaggerUiSettingsBuilder(swaggerUiSettings);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "swagger";
                c.SwaggerEndpoint("v1/swagger.json", "API v1");

                c.DocumentTitle = swaggerUiSettings.ApiTitle;
                c.DocExpansion(swaggerUiSettings.ApiDocExpansion ?? DocExpansion.None);
            });

            return app;
        }
    }

    public class SwaggerSettings
    {
        public string ApiTitle { get; set; }
        public string ApiDescription { get; set; }
        public string ApiContactInfo { get; set; }
        public bool SecureWithUseJwtAuth { get; set; }
    }

    public class SwaggerUiSettings
    {
        public string ApiTitle { get; set; }
        public DocExpansion? ApiDocExpansion { get; set; }
    }
}