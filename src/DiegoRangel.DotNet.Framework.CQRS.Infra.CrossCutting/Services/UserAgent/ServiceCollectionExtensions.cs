using Microsoft.Extensions.DependencyInjection;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.UserAgent
{
    public static class ServiceCollectionExtensions
    {
        public static void AddUserAgentService(this IServiceCollection services)
        {
            services.AddScoped<IUserAgentService, UserAgentService>();
        }
    }
}