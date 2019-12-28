namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Session
{
    public interface ILoggedInUserProvider 
    {
        TUser GetLoggedInUser<TUser, TKey>() 
            where TUser : IUser<TKey>
            where TKey : struct;
    }
}