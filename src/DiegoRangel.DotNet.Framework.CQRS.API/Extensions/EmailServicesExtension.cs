using System;
using DiegoRangel.DotNet.Framework.CQRS.API.Services.Email;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Email;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Extensions
{
    public static class EmailServicesExtension
    {
        public static void AddEmailServices(this IServiceCollection services, IWebHostEnvironment env, Action<EmailSettings> settingsBuilder)
        {
            services.AddScoped<IEmailQueue, EmailQueue>();
            services.AddScoped<IEmailTemplateProvider, EmailTemplateProvider>();

            if (env.IsDevelopment())
                services.AddScoped<IEmailSenderService, ConsoleEmailSenderService>();
            else
                services.AddScoped<IEmailSenderService, EmailSenderService>();

            var settings = new EmailSettings();
            settingsBuilder(settings);
            services.AddSingleton(settings);
        }
    }
}