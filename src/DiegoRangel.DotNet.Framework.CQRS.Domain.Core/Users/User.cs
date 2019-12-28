using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Session;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Users
{
    public abstract class User<TUserPrimaryKey> : Entity<TUserPrimaryKey>, IUser<TUserPrimaryKey>
    {
        
    }

    public abstract class User : User<int>
    {

    }
}