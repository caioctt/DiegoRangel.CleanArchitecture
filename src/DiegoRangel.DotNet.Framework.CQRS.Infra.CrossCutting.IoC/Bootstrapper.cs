using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Interfaces;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Notifications;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.IoC.Extensions;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Security;
using DiegoRangel.DotNet.Framework.CQRS.Infra.Data.EFCore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.IoC
{
    public static class Bootstrapper
    {
        public static void RegisterServicesBasedOn(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IRandomizeProvider, RandomizeProvider>();
            services.AddSingleton<ITokenProvider, TokenProvider>();

            services.AddScoped<IEntitiesAuditer, EntitiesAuditer>();
            services.AddScoped<DomainNotificationContext>();

            services.RegisterStateMachines();
            services.RegisterWhoImplements(typeof(IRepository));
            services.RegisterWhoImplements(typeof(IDomainService));
        }
    }
}