using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Extensions
{
    public static class SwaggerExtensions
    {
        public static void AddSwaggerConfig(this IServiceCollection services, string apiName, string contactInfo, bool useJwtAuth)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API",
                    Description = apiName,
                    Contact = new OpenApiContact { Name = contactInfo }
                });

                if (!useJwtAuth) return;

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

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app, string documentTitle)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "swagger";
                c.SwaggerEndpoint("v1/swagger.json", "API v1");

                c.DocumentTitle = documentTitle;
                c.DocExpansion(DocExpansion.None);
            });

            return app;
        }
    }
}