using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Session;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing
{
    /// <summary>
    /// A shortcut of <see cref="IFullAudited{TUserPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    public interface IFullAudited : IFullAudited<int>
    {

    }

    /// <summary>
    /// This interface is implemented by entities which must be full audited.
    /// </summary>
    /// <typeparam name="TUserPrimaryKey">The user's primary key type</typeparam>
    public interface IFullAudited<TUserPrimaryKey> : IAudited<TUserPrimaryKey>, IDeletionAudited<TUserPrimaryKey>
    {

    }

    /// <summary>
    /// Adds navigation properties to <see cref="IFullAudited"/> interface for user.
    /// </summary>
    /// <typeparam name="TUser">Type of the user</typeparam>
    /// <typeparam name="TUserPrimaryKey">The user's primary key type</typeparam>
    public interface IFullAudited<TUser, TUserPrimaryKey> : IFullAudited<TUserPrimaryKey>, IAudited<TUser, TUserPrimaryKey>, IDeletionAudited<TUser, TUserPrimaryKey>
        where TUser : IEntity<TUserPrimaryKey>, IUser<TUserPrimaryKey>
    {

    }
}