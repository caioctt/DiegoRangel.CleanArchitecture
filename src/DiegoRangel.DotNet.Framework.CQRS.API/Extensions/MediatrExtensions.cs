using System;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Extensions
{
    public static class MediatrExtensions
    {
        public static void AddMediatr(this IServiceCollection services, Type assemblyScanner)
        {
            var assembly = AppDomain.CurrentDomain.Load(assemblyScanner.Namespace);
            services.AddMediatR(assembly);
        }
    }
}