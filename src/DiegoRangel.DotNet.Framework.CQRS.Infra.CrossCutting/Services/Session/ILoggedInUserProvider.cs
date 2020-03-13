using System.Threading.Tasks;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Session
{
    public interface ILoggedInUserProvider<TUser, TUserPrimaryKey>
        where TUser : IUser<TUserPrimaryKey>
    {
        Task<TUser> GetUserLoggedInAsync();
    }
}