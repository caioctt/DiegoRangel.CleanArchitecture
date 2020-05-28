using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.AutoInjector;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.UserAgent
{
    public interface IUserAgentService : IService
    {
        IUserAgentData GetCurrentUserAgentData();
    }
}