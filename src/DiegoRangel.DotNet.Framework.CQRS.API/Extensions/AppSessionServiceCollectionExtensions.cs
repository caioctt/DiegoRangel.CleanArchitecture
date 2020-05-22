using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Session;
using Microsoft.Extensions.DependencyInjection;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Extensions
{
    public static class AppSessionServiceCollectionExtensions
    {
        public static void AddUserSignedInServices<TUser, TUserKey, TLoggedInUserProvider, TLoggedInUserIdProvider, TLoggedInUserIdentifierProvider>(this IServiceCollection services)
            where TLoggedInUserIdProvider : class, ILoggedInUserIdProvider<TUserKey>
            where TLoggedInUserProvider : class, ILoggedInUserProvider<TUser, TUserKey>
            where TLoggedInUserIdentifierProvider : class, ILoggedInUserIdentifierProvider
            where TUser : IUser<TUserKey>
        {
            services.AddScoped<ILoggedInUserProvider<TUser, TUserKey>, TLoggedInUserProvider>();
            services.AddScoped<ILoggedInUserIdProvider<TUserKey>, TLoggedInUserIdProvider>();
            services.AddScoped<ILoggedInUserIdentifierProvider, TLoggedInUserIdentifierProvider>();
        }
    }
}