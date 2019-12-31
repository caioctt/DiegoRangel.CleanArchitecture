using System.Reflection;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Interfaces;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Notifications;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.IoC.Extensions;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.IoC
{
    public static class Bootstrapper
    {
        public static void RegisterServicesBasedOn<TUserPrimaryKey>(IServiceCollection services, params Assembly[] assemblies)
            where TUserPrimaryKey : struct
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IRandomizeProvider, RandomizeProvider>();
            services.AddSingleton<ITokenProvider, TokenProvider>();

            services.AddScoped<IAuditManager, AuditManager<TUserPrimaryKey>>();
            services.AddScoped<DomainNotificationContext>();

            services.RegisterStateMachines(assemblies);
            services.RegisterWhoImplements(typeof(IRepository), assemblies);
            services.RegisterWhoImplements(typeof(IDomainService), assemblies);
            services.RegisterWhoImplements(typeof(IService), assemblies);
        }
    }
}