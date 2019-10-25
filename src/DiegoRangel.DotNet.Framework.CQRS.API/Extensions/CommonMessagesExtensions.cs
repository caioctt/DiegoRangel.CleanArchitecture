using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Extensions
{
    public static class CommonMessagesExtensions
    {
        public static void AddCommonMessages(this IServiceCollection services, CommonMessages commonMessages)
        {
            services.AddSingleton(commonMessages);
        }
    }
}