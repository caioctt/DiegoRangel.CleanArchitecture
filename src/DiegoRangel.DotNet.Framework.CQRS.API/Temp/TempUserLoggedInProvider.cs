using System;
using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Session;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Temp
{
    public class TempLoggedInUserProvider : ILoggedInUserProvider<TempUser, Guid>
    {
        public Task<TempUser> GetUserLoggedInAsync()
        {
            return Task.FromResult(new TempUser());
        }
    }
    public class TempLoggedInUserIdProvider : ILoggedInUserIdProvider<Guid>
    {
        public Task<Guid> GetUserLoggedInIdAsync()
        {
            return Task.FromResult(Guid.NewGuid());
        }
    }
    public class TempLoggedInUserIdentifierProvider : ILoggedInUserIdentifierProvider
    {
        public Task<string> GetUserIdentifierAsync()
        {
            return Task.FromResult(Guid.NewGuid().ToString());
        }
    }
}