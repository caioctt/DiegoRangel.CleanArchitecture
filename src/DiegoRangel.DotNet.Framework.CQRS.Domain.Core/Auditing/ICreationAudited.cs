using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Session;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing
{
    /// <summary>
    /// A shortcut of <see cref="ICreationAudited{TEntityPrimaryKey, TUserPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    public interface ICreationAudited : ICreationAudited<int, int>
    {

    }

    /// <summary>
    /// This interface is implemented by entities that is wanted to store creation information (who and when created).
    /// Creation time and creator user are automatically set when saving <see cref="Entity"/> to database.
    /// </summary>
    /// <typeparam name="TEntityPrimaryKey">The entity's key type</typeparam>
    /// <typeparam name="TUserPrimaryKey">The user's primary key type</typeparam>
    public interface ICreationAudited<TEntityPrimaryKey, TUserPrimaryKey> : 
        IEntity<TEntityPrimaryKey>, IHasCreationTime
    {
        /// <summary>
        /// Id of the creator user of this entity.
        /// </summary>
        TUserPrimaryKey CreatorUserId { get; set; }
    }

    /// <summary>
    /// Adds navigation properties to <see cref="ICreationAudited{TUser,TUserPrimaryKey}"/> interface for user.
    /// </summary>
    /// <typeparam name="TEntityPrimaryKey">The entity's key type</typeparam>
    /// <typeparam name="TUserPrimaryKey">The user's primary key type</typeparam>
    /// <typeparam name="TUser">Type of the user</typeparam>
    public interface ICreationAudited<TEntityPrimaryKey, TUserPrimaryKey, TUser> : 
        ICreationAudited<TEntityPrimaryKey, TUserPrimaryKey>
        where TUser : IEntity<TUserPrimaryKey>, IUser<TUserPrimaryKey>
    {
        /// <summary>
        /// Reference to the creator user of this entity.
        /// </summary>
        TUser CreatorUser { get; set; }
    }
}