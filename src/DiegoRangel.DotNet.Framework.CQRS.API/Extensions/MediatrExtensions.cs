using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Extensions
{
    public static class MediatrExtensions
    {
        public static void AddMediatr(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddMediatR(assemblies);
        }
    }
}