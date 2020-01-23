using System;
using DiegoRangel.DotNet.Framework.CQRS.API.Extensions;
using DiegoRangel.DotNet.Framework.CQRS.API.Services.Session;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.IoC;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.IoC.Extensions;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Messages;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Session;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace DiegoRangel.DotNet.Framework.CQRS.API
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        public Startup(IWebHostEnvironment env)
        {
            _env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var assemblies = new[]
            {
                typeof(CommonMessages).Assembly,
                typeof(Domain.Core.Entities.Entity).Assembly,
                typeof(Infra.Data.EFCore.Setup.CustomDbContextOptionsBuilder).Assembly,
                typeof(Infra.Data.MongoDB.Context.MongoDbContext).Assembly,
            };

            services.AddControllers();

            services.AddCulture("pt-BR");
            services.AddHealthChecks();
            services.AddCacheServices();
            services.AddCompression();
            services.AddIOServices();
            services.AddMediatr(assemblies);

            services.AddSwaggerDocumentation(settings =>
            {
                settings.ApiTitle = "My test api Title";
                settings.ApiDescription = "My test api description";
                settings.ApiContactInfo = "no-reply@gmail.com";
                settings.SecureWithUseJwtAuth = false;
            });

            services.AddCommonMessages(messages =>
            {
                messages.NotFound = "Oops! Recurso não encontrado.";
                messages.InvalidOperation = "Oops! Operação inválida. Recarregue sua tela e tente novamente.";
                messages.UnhandledOperation = "Oops! Não foi possível processar a sua solicitação no momento, tente novamente mais tarde.";
            });

            services.AddEmailServices(_env, settings =>
            {
                settings.Host = "smtp.gmail.com";
                settings.NoReplyMail = "no-reply@gmail.com";
                settings.UserName = "no-reply@gmail.com";
                settings.Password = "123456";
                settings.Port = 587;
                settings.EnableSsl = true;
                settings.UseDefaultCredentials = false;
            });

            services.AddAutoMapperWithSettings(settings =>
            {
                settings.UseStringTrimmingTransformers = true;
            }, assemblies);

            services.AddEfCoreServices();
            services.AddMongoDb(settings =>{});

            Bootstrapper.RegisterServicesBasedOn<Guid>(services, assemblies);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseRouting();
            app.UseExceptionHandlers();
            app.UseResponseCompression();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });

            app.UseSwaggerDocumentation(settings =>
            {
                settings.ApiTitle = "My test api Title";
                settings.ApiDocExpansion = DocExpansion.Full;
            });
        }
    }
}
