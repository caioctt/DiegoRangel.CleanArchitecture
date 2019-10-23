using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Interfaces;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Notifications;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.IoC.Extensions;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Security;
using DiegoRangel.DotNet.Framework.CQRS.Infra.Data.EFCore.Services;
using DiegoRangel.DotNet.Framework.CQRS.Infra.Data.EFCore.UoW;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.IoC
{
    public static class Bootstrapper
    {
        public static void RegisterServicesBasedOn(IServiceCollection services, string projectName)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IRandomizeProvider, RandomizeProvider>();
            services.AddSingleton<ITokenProvider, TokenProvider>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEntitiesAuditer, EntitiesAuditer>();
            services.AddScoped<DomainNotificationContext>();

            services.RegisterStateMachines(projectName);
            services.RegisterWhoImplements(typeof(IRepository), projectName);
            services.RegisterWhoImplements(typeof(IDomainService), projectName);
        }
    }
}