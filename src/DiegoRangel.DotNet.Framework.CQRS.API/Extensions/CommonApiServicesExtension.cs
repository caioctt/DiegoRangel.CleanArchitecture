using DiegoRangel.DotNet.Framework.CQRS.API.Services.Cache;
using DiegoRangel.DotNet.Framework.CQRS.API.Services.Email;
using DiegoRangel.DotNet.Framework.CQRS.API.Services.IO;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Cache;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Email;
using Microsoft.Extensions.DependencyInjection;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Extensions
{
    public static class CommonApiServicesExtension
    {
        public static void AddCommonApiServices(this IServiceCollection services)
        {
            services.AddScoped<IEmailTemplateProvider, EmailTemplateProvider>();
            services.AddScoped<IFilesService, FilesService>();
            services.AddSingleton<ICacheService, CacheService>();
        }
    }
}