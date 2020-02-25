using System;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Session;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Temp
{
    public class TempUser : IUser<Guid>
    {
        public Guid Id { get; set; }
    }
}