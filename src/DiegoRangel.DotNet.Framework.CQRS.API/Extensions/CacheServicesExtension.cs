using DiegoRangel.DotNet.Framework.CQRS.API.Services.Cache;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Cache;
using Microsoft.Extensions.DependencyInjection;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Extensions
{
    public static class CacheServicesExtension
    {
        public static void AddCacheServices(this IServiceCollection services)
        {
            services.AddSingleton<ICacheService, CacheService>();
        }
    }
}