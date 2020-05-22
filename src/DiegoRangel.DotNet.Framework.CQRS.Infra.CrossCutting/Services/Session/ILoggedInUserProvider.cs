using System.Threading.Tasks;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Session
{
    public interface ILoggedInUserProvider<TUser, TUserKey>
        where TUser : IUser<TUserKey>
    {
        Task<TUser> GetUserLoggedInAsync();
    }
}