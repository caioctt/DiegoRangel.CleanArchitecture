using DiegoRangel.DotNet.Framework.CQRS.Infra.Data.EFCore.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.Data.EFCore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddEfCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IChangeTrackerAuditer, ChangeTrackerAuditer>();
        }
    }
}