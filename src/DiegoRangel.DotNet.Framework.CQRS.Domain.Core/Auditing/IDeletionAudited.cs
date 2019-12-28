using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Session;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing
{
    /// <summary>
    /// A shortcut of <see cref="IDeletionAudited{TEntityPrimaryKey, TUserPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    public interface IDeletionAudited : IDeletionAudited<int, int>
    {

    }

    /// <summary>
    /// This interface is implemented by entities which wanted to store deletion information (who and when deleted).
    /// </summary>
    /// <typeparam name="TEntityPrimaryKey">The entity's key type</typeparam>
    /// <typeparam name="TUserPrimaryKey">The user's primary key type</typeparam>
    public interface IDeletionAudited<TEntityPrimaryKey, TUserPrimaryKey> : 
        IEntity<TEntityPrimaryKey>, IHasDeletionTime
    {
        /// <summary>
        /// Which user deleted this entity?
        /// </summary>
        TUserPrimaryKey DeleterUserId { get; set; }
    }

    /// <summary>
    /// Adds navigation properties to <see cref="IDeletionAudited{TUser,TUserPrimaryKey}"/> interface for user.
    /// </summary>
    /// <typeparam name="TEntityPrimaryKey">The entity's key type</typeparam>
    /// <typeparam name="TUserPrimaryKey">The user's primary key type</typeparam>
    /// <typeparam name="TUser">Type of the user</typeparam>
    public interface IDeletionAudited<TEntityPrimaryKey, TUserPrimaryKey, TUser> : 
        IDeletionAudited<TEntityPrimaryKey, TUserPrimaryKey>
        where TUser : IEntity<TUserPrimaryKey>, IUser<TUserPrimaryKey>
    {
        /// <summary>
        /// Reference to the deleter user of this entity.
        /// </summary>
        TUser DeleterUser { get; set; }
    }
}