using System;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Extensions
{
    public static class MediatrExtensions
    {
        public static void AddMediatr(this IServiceCollection services, string projectName)
        {
            var assembly = AppDomain.CurrentDomain.Load(projectName);
            services.AddMediatR(assembly);
        }
    }
}