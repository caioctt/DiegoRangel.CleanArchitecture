using System;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Session;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing
{
    /// <summary>
    /// A shortcut of <see cref="FullAuditedEntity{TEntityPrimaryKey, TUserKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    public abstract class FullAuditedEntity : FullAuditedEntity<int, int>
    {

    }

    /// <summary>
    /// Implements <see cref="IFullAudited"/> to be a base class for full-audited entities.
    /// </summary>
    /// <typeparam name="TEntityPrimaryKey">The entity's key type</typeparam>
    /// <typeparam name="TUserKey">The user's primary key type</typeparam>
    public abstract class FullAuditedEntity<TEntityPrimaryKey, TUserKey> : 
        AuditedEntity<TEntityPrimaryKey, TUserKey>, 
        IFullAudited<TEntityPrimaryKey, TUserKey>
        where TUserKey : struct
    {
        /// <summary>
        /// Is this entity Deleted?
        /// </summary>
        public virtual bool IsDeleted { get; private set; }

        public void MoveToTrash()
        {
            IsDeleted = true;
        }

        /// <summary>
        /// Which user deleted this entity?
        /// </summary>
        public virtual TUserKey? DeleterUserId { get; set; }

        /// <summary>
        /// Deletion time of this entity.
        /// </summary>
        public virtual DateTime? DeletionTime { get; set; }
    }

    /// <summary>
    /// Implements <see cref="IFullAudited{TEntityPrimaryKey, TUserKey, TUser}"/> to be a base class for full-audited entities.
    /// </summary>
    /// <typeparam name="TEntityPrimaryKey">The entity's key type</typeparam>
    /// <typeparam name="TUserKey">The user's primary key type</typeparam>
    /// <typeparam name="TUser">Type of the user</typeparam>
    public abstract class FullAuditedEntity<TEntityPrimaryKey, TUserKey, TUser> : 
        FullAuditedEntity<TEntityPrimaryKey, TUserKey>, 
        IFullAudited<TEntityPrimaryKey, TUserKey, TUser>
        where TUser : IEntity<TUserKey>, IUser<TUserKey>
        where TUserKey : struct
    {
        /// <summary>
        /// Reference to the creator user of this entity.
        /// </summary>
        public virtual TUser CreatorUser { get; set; }

        /// <summary>
        /// Reference to the last modifier user of this entity.
        /// </summary>
        public virtual TUser LastModifierUser { get; set; }

        /// <summary>
        /// Reference to the deleter user of this entity.
        /// </summary>
        public virtual TUser DeleterUser { get; set; }
    }
}