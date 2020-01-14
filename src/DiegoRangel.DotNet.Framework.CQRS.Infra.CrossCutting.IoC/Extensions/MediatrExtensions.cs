using System.Reflection;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.MediatR;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.IoC.Extensions
{
    public static class MediatrExtensions
    {
        public static void AddMediatr(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddMediatR(assemblies);
            services.AddSingleton(new MediatrAssemblies(assemblies));
        }
    }
}