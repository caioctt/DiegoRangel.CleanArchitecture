using System;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Session;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Temp
{
    public class TempUserLoggedInProvider : ILoggedInUserProvider<TempUser, Guid>
    {
        public TempUser GetLoggedInUser()
        {
            return new TempUser();
        }
    }
}