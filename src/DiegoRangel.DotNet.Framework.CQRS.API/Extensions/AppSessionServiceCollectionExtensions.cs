using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Session;
using Microsoft.Extensions.DependencyInjection;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Extensions
{
    public static class AppSessionServiceCollectionExtensions
    {
        public static void AddUserSignedInServices<TUser, TUserPrimaryKey, TLoggedInUserProvider, TLoggedInUserIdProvider, TLoggedInUserIdentifierProvider>(this IServiceCollection services)
            where TLoggedInUserIdProvider : class, ILoggedInUserIdProvider<TUserPrimaryKey>
            where TLoggedInUserProvider : class, ILoggedInUserProvider<TUser, TUserPrimaryKey>
            where TLoggedInUserIdentifierProvider : class, ILoggedInUserIdentifierProvider
            where TUser : IUser<TUserPrimaryKey>
        {
            services.AddScoped<ILoggedInUserProvider<TUser, TUserPrimaryKey>, TLoggedInUserProvider>();
            services.AddScoped<ILoggedInUserIdProvider<TUserPrimaryKey>, TLoggedInUserIdProvider>();
            services.AddScoped<ILoggedInUserIdentifierProvider, TLoggedInUserIdentifierProvider>();
        }
    }
}