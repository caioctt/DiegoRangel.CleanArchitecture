using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Session;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing
{
    /// <summary>
    /// A shortcut of <see cref="IFullAudited{TEntityPrimaryKey, TUserKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    public interface IFullAudited : IFullAudited<int, int>
    {

    }

    /// <summary>
    /// This interface is implemented by entities which must be full audited.
    /// </summary>
    /// <typeparam name="TEntityPrimaryKey">The entity's key type</typeparam>
    /// <typeparam name="TUserKey">The user's primary key type</typeparam>
    public interface IFullAudited<TEntityPrimaryKey, TUserKey> : 
        IAudited<TEntityPrimaryKey, TUserKey>, 
        IDeletionAudited<TEntityPrimaryKey, TUserKey>
    {

    }

    /// <summary>
    /// Adds navigation properties to <see cref="IFullAudited"/> interface for user.
    /// </summary>
    /// <typeparam name="TEntityPrimaryKey">The entity's key type</typeparam>
    /// <typeparam name="TUserKey">The user's primary key type</typeparam>
    /// <typeparam name="TUser">Type of the user</typeparam>
    public interface IFullAudited<TEntityPrimaryKey, TUserKey, TUser> : 
        IFullAudited<TEntityPrimaryKey, TUserKey>, 
        IAudited<TEntityPrimaryKey, TUserKey, TUser>, 
        IDeletionAudited<TEntityPrimaryKey, TUserKey, TUser>
        where TUser : IEntity<TUserKey>, IUser<TUserKey>
    {

    }
}