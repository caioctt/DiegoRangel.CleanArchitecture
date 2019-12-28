using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Session;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing
{
    /// <summary>
    /// A shortcut of <see cref="IModificationAudited{TEntityPrimaryKey, TUserPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    public interface IModificationAudited : IModificationAudited<int, int>
    {

    }

    /// <summary>
    /// This interface is implemented by entities that is wanted to store modification information (who and when modified lastly).
    /// Properties are automatically set when updating the <see cref="IEntity"/>.
    /// </summary>
    /// <typeparam name="TEntityPrimaryKey">The entity's key type</typeparam>
    /// <typeparam name="TUserPrimaryKey">The user's primary key type</typeparam>
    public interface IModificationAudited<TEntityPrimaryKey, TUserPrimaryKey> : 
        IEntity<TEntityPrimaryKey>, IHasModificationTime
    {
        /// <summary>
        /// Last modifier user for this entity.
        /// </summary>
        TUserPrimaryKey LastModifierUserId { get; set; }
    }

    /// <summary>
    /// Adds navigation properties to <see cref="IModificationAudited{TUser,TUserPrimaryKey}"/> interface for user.
    /// </summary>
    /// <typeparam name="TEntityPrimaryKey">The entity's key type</typeparam>
    /// <typeparam name="TUserPrimaryKey">The user's primary key type</typeparam>
    /// <typeparam name="TUser">Type of the user</typeparam>
    public interface IModificationAudited<TEntityPrimaryKey, TUserPrimaryKey, TUser> : 
        IModificationAudited<TEntityPrimaryKey, TUserPrimaryKey>
        where TUser : IEntity<TUserPrimaryKey>, IUser<TUserPrimaryKey>
    {
        /// <summary>
        /// Reference to the last modifier user of this entity.
        /// </summary>
        TUser LastModifierUser { get; set; }
    }
}