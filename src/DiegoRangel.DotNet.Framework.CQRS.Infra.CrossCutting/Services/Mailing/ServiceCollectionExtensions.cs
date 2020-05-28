using System;
using Microsoft.Extensions.DependencyInjection;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Mailing
{
    public static class ServiceCollectionExtensions
    {
        public static FluentEmailServicesBuilder AddMailServices(this IServiceCollection services, Func<bool> isDevelopment, Func<MailSettings> setupMailSettings)
        {
            var mailSettings = setupMailSettings();
            services.AddSingleton(mailSettings);
            services.AddScoped<IEmailQueue, EmailQueue>();

            var builder = services
                .AddFluentEmail(mailSettings.DefaultFromEmail, mailSettings.DefaultFromName)
                .AddSmtpSender(mailSettings.Host, mailSettings.Port, mailSettings.Username, mailSettings.Password);

            if (isDevelopment())
                services.AddScoped<IEmailSender, ConsoleEmailSender>();
            else
                services.AddScoped<IEmailSender, EmailSender>();

            return builder;
        }

        public static FluentEmailServicesBuilder WithTemplatingRenderers(this FluentEmailServicesBuilder builder, IServiceCollection services, Action<MailTemplatingSettings> setupMailTemplatingSettings)
        {
            var mailTemplatingSettings = new MailTemplatingSettings();
            setupMailTemplatingSettings(mailTemplatingSettings);
            services.AddSingleton(mailTemplatingSettings);

            builder.AddRazorRenderer(mailTemplatingSettings.EmailTemplatesDiscoveryType);
            return builder;
        }
    }
}