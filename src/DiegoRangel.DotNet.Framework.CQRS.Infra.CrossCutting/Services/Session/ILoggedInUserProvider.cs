namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Session
{
    public interface ILoggedInUserProvider
    {
        IUser<TUserPrimaryKey> GetLoggedInUser<TUserPrimaryKey>() where TUserPrimaryKey : struct;
    }
}