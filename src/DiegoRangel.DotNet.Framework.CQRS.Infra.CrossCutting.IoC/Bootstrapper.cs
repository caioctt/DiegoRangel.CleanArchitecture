using System.Reflection;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Interfaces;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.IoC.Extensions;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.AutoInjector;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.NotificationPattern;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.IoC
{
    public static class Bootstrapper
    {
        public static void RegisterServicesBasedOn<TUserKey>(IServiceCollection services, params Assembly[] assemblies)
            where TUserKey : struct
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IRandomizeProvider, RandomizeProvider>();
            services.AddSingleton<ITokenProvider, TokenProvider>();

            services.AddScoped<IAuditManager, AuditManager<TUserKey>>();
            services.AddScoped<INotificationContext, NotificationContext>();

            services.RegisterStateMachines(assemblies);
            services.RegisterWhoImplements(typeof(IRepository), assemblies);
            services.RegisterWhoImplements(typeof(IDomainService), assemblies);
            services.RegisterWhoImplements(typeof(IService), assemblies);
            services.RegisterWhoImplements(typeof(ITransientService), assemblies, ServiceLifetime.Transient);
            services.RegisterWhoImplements(typeof(ISingletonService), assemblies, ServiceLifetime.Singleton);
            services.RegisterWhoImplements(typeof(IUnitOfWork), assemblies);
        }
    }
}