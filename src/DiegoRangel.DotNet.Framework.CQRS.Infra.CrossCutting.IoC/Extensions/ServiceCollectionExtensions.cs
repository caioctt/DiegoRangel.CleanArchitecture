using System;
using System.Linq;
using System.Reflection;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.IoC.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterWhoImplements(this IServiceCollection service, Type interfaceType, Assembly[] assemblies, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            var types = assemblies.SelectMany(x => x.GetTypes()).ToList();

            var interfaces = types
                .Where(x => !x.IsClass && x.GetInterfaces().Contains(interfaceType)).Select(x => x).ToList();

            foreach (var i in interfaces)
            {
                var classe = types.Where(x =>
                    x.IsClass && !x.IsAbstract && x.GetInterfaces().Contains(interfaceType) &&
                    x.GetInterfaces().Contains(i)).Select(x => x).FirstOrDefault();

                if (classe == null)
                    continue;

                switch (lifetime)
                {
                    case ServiceLifetime.Singleton:
                        service.AddSingleton(i, classe);
                        break;
                    case ServiceLifetime.Scoped:
                        service.AddScoped(i, classe);
                        break;
                    case ServiceLifetime.Transient:
                        service.AddTransient(i, classe);
                        break;
                }
            }

            return service;
        }

        public static void RegisterStateMachines(this IServiceCollection service, params Assembly[] assemblies)
        {
            var types = assemblies.SelectMany(x => x.GetTypes()).ToList();
            var stateMachines = types
                .Where(x =>
                    x.IsClass && !x.IsAbstract && x.GetInterfaces().Contains(typeof(IStateMachine))
                ).Select(x => x).ToList();

            foreach (var x in stateMachines)
            {
                service.AddScoped(x);
            }
        }
    }
}