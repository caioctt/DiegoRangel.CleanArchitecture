using System;
using DiegoRangel.DotNet.Framework.CQRS.API.Extensions;
using DiegoRangel.DotNet.Framework.CQRS.API.Filters;
using DiegoRangel.DotNet.Framework.CQRS.API.Temp;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.IoC;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.MediatR.Extensions;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Messages;
using DiegoRangel.DotNet.Framework.CQRS.Infra.Data.EFCore.Extensions;
using DiegoRangel.DotNet.Framework.CQRS.Infra.Data.MongoDB.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace DiegoRangel.DotNet.Framework.CQRS.API
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var assemblies = new[]
            {
                typeof(CommonMessages).Assembly,
                typeof(Domain.Core.Entities.Entity).Assembly,
                typeof(Infra.Data.EFCore.Setup.CustomDbContextOptionsBuilder).Assembly,
                typeof(Infra.Data.MongoDB.Context.MongoDbContext).Assembly,
            };

            services.AddControllers(options => { options.Filters.Add<ResponseValidationFilter>(); })
                .AddNewtonsoftJson();

            services.AddCulture("pt-BR");
            services.AddHealthChecks();
            services.AddCacheServices();
            services.AddCompression();
            services.AddIOServices();
            services.AddMediatr(assemblies)
                .AddRequestPerformanceBehavior()
                .AddUnhandledExceptionBehavior();

            services.AddSwaggerDocumentation(() => new SwaggerSettings
            {
                ApiTitle = "My test api Title",
                ApiDescription = "My test api description",
                ApiContactInfo = "no-reply@gmail.com",
                SecureWithUseJwtAuth = false
            });

            services.AddCommonMessages(() => new CommonMessages
            {
                NotFound = "Oops! Not found.",
                InvalidOperation = "Oops! Invalid operation.",
                UnhandledOperation = "Oops! We were unable to process your request at this time, please try again later."
            });

            services.AddAutoMapperWithSettings(settings =>
            {
                settings.UseStringTrimmingTransformers = true;
            }, assemblies);

            services.AddEfCoreServices();
            services.AddMongoDb(settings =>{});

            services.AddUserSignedInServices<TempUser, Guid,
                TempLoggedInUserProvider,
                TempLoggedInUserIdProvider,
                TempLoggedInUserIdentifierProvider>();

            Bootstrapper.RegisterServicesBasedOn<Guid>(services, assemblies);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });

            app.UseRouting();
            app.UseExceptionHandlers();
            app.UseResponseCompression();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });

            app.UseSwaggerDocumentation(() => new SwaggerUiSettings
            {
                ApiTitle = "My test api Title",
                ApiDocExpansion = DocExpansion.Full
            });
        }
    }
}
