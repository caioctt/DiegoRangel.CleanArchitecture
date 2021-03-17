using System;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Session;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing
{
    /// <summary>
    /// A shortcut of <see cref="IDeletionAudited{TEntityPrimaryKey, TUserKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    public interface IDeletionAudited : IDeletionAudited<int, int>
    {

    }

    /// <summary>
    /// This interface is implemented by entities which wanted to store deletion information (who and when deleted).
    /// </summary>
    /// <typeparam name="TEntityPrimaryKey">The entity's key type</typeparam>
    /// <typeparam name="TUserKey">The user's primary key type</typeparam>
    public interface IDeletionAudited<TEntityPrimaryKey, TUserKey> : IEntity<TEntityPrimaryKey>, IHasDeletionTime
    {
        /// <summary>
        /// Which user deleted this entity?
        /// </summary>
        TUserKey DeleterUserId { get; set; }
    }

    /// <summary>
    /// Adds navigation properties to <see cref="IDeletionAudited{TUser,TUserKey}"/> interface for user.
    /// </summary>
    /// <typeparam name="TEntityPrimaryKey">The entity's key type</typeparam>
    /// <typeparam name="TUserKey">The user's primary key type</typeparam>
    /// <typeparam name="TUser">Type of the user</typeparam>
    public interface IDeletionAudited<TEntityPrimaryKey, TUserKey, TUser> : IDeletionAudited<TEntityPrimaryKey, TUserKey>
        where TUser : IEntity<TUserKey>, IUser<TUserKey>
    {
        /// <summary>
        /// Reference to the deleter user of this entity.
        /// </summary>
        TUser DeleterUser { get; set; }
    }
}