namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Session
{
    public interface ILoggedInUser
    {
        IUser GetLoggedInUser();
    }
}