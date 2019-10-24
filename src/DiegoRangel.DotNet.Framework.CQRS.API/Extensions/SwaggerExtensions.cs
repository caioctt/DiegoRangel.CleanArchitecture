using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Extensions
{
    public static class SwaggerExtensions
    {
        public static void AddSwaggerConfig(this IServiceCollection services, string apiName, string contactInfo)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "API",
                    Description = apiName,
                    Contact = new Contact { Name = contactInfo }
                });

                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                c.AddSecurityRequirement(security);
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