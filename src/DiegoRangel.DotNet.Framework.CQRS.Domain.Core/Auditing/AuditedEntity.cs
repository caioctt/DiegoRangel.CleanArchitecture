using System;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Session;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing
{
    /// <summary>
    /// A shortcut of <see cref="AuditedEntity{TEntityPrimaryKey, TUserKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    public abstract class AuditedEntity : AuditedEntity<int, int>
    {

    }

    /// <summary>
    /// This class can be used to simplify implementing <see cref="IAudited"/>.
    /// </summary>
    /// <typeparam name="TEntityPrimaryKey">The entity's key type</typeparam>
    /// <typeparam name="TUserKey">The user's primary key type</typeparam>
    public abstract class AuditedEntity<TEntityPrimaryKey, TUserKey> : 
        CreationAuditedEntity<TEntityPrimaryKey, TUserKey>, 
        IAudited<TEntityPrimaryKey, TUserKey>
    {
        /// <summary>
        /// Last modification date of this entity.
        /// </summary>
        public virtual DateTime LastModificationTime { get; set; }

        /// <summary>
        /// Last modifier user of this entity.
        /// </summary>
        public virtual TUserKey LastModifierUserId { get; set; }
    }

    /// <summary>
    /// This class can be used to simplify implementing <see cref="IAudited{TEntityPrimaryKey, TUserKey, TUser}"/>.
    /// </summary>
    /// <typeparam name="TEntityPrimaryKey">The entity's key type</typeparam>
    /// <typeparam name="TUserKey">The user's primary key type</typeparam>
    /// <typeparam name="TUser">Type of the user</typeparam>
    public abstract class AuditedEntity<TEntityPrimaryKey, TUserKey, TUser> : 
        AuditedEntity<TEntityPrimaryKey, TUserKey>,
        IAudited<TEntityPrimaryKey, TUserKey, TUser>
        where TUser : IEntity<TUserKey>, IUser<TUserKey>
    {
        /// <summary>
        /// Reference to the creator user of this entity.
        /// </summary>
        public virtual TUser CreatorUser { get; set; }

        /// <summary>
        /// Reference to the last modifier user of this entity.
        /// </summary>
        public virtual TUser LastModifierUser { get; set; }
    }
}