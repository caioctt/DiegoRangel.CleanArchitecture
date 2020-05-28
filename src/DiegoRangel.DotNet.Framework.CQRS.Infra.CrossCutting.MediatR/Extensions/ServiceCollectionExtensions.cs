using System.Reflection;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.MediatR.Behaviors;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.MediatR.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMediatr(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddMediatR(assemblies);
            services.AddSingleton(new MediatrAssemblies(assemblies));
            return services;
        }

        public static IServiceCollection AddRequestLoggerBehavior(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRequestPreProcessor<>), typeof(RequestLoggerBehavior<>));
            return services;
        }
        public static IServiceCollection AddRequestPerformanceBehavior(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehavior<,>));
            return services;
        }
        public static IServiceCollection AddUnhandledExceptionBehavior(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
            return services;
        }
    }
}