using System;
using Microsoft.Extensions.DependencyInjection;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.SMS
{
    public static class ServiceCollectionExtensions
    {
        public static void AddTwilio(this IServiceCollection services, Func<bool> isDevelopment, Func<TwilioSettings> setupAction)
        {
            var settings = setupAction();
            services.AddSingleton(settings);

            if (isDevelopment())
                services.AddScoped<ISmsSender, ConsoleSmsSender>();
            else
                services.AddScoped<ISmsSender, SmsSender>();
        }
    }
}