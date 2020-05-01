using System.Threading.Tasks;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Session
{
    public interface ILoggedInUserIdProvider<TUserPrimaryKey>
    {
        Task<TUserPrimaryKey> GetUserLoggedInIdAsync();
    }
    public interface ILoggedInUserIdentifierProvider
    {
        Task<string> GetUserIdentifierAsync();
    }
}