namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Session
{
    public interface ILoggedInUserProvider<out TUser, TUserPrimaryKey>
        where TUser : IUser<TUserPrimaryKey>
    {
        TUser GetLoggedInUser();
    }
}