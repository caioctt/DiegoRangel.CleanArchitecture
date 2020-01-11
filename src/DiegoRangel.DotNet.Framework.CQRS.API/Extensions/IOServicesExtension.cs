using DiegoRangel.DotNet.Framework.CQRS.API.Services.IO;
using Microsoft.Extensions.DependencyInjection;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Extensions
{
    public static class IOServicesExtension
    {
        public static void AddIOServices(this IServiceCollection services)
        {
            services.AddScoped<IFilesService, FilesService>();
        }
    }
}