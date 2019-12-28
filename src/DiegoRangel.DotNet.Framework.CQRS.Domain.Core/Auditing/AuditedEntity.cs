using System;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Session;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing
{
    /// <summary>
    /// A shortcut of <see cref="AuditedEntity{TEntityPrimaryKey, TUserPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    public abstract class AuditedEntity : AuditedEntity<int, int>
    {

    }

    /// <summary>
    /// This class can be used to simplify implementing <see cref="IAudited"/>.
    /// </summary>
    /// <typeparam name="TEntityPrimaryKey">The entity's key type</typeparam>
    /// <typeparam name="TUserPrimaryKey">The user's primary key type</typeparam>
    public abstract class AuditedEntity<TEntityPrimaryKey, TUserPrimaryKey> : 
        CreationAuditedEntity<TEntityPrimaryKey, TUserPrimaryKey>, 
        IModificationAudited<TEntityPrimaryKey, TUserPrimaryKey>
    {
        /// <summary>
        /// Last modification date of this entity.
        /// </summary>
        public virtual DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// Last modifier user of this entity.
        /// </summary>
        public virtual TUserPrimaryKey LastModifierUserId { get; set; }
    }

    /// <summary>
    /// This class can be used to simplify implementing <see cref="IAudited{TEntityPrimaryKey, TUserPrimaryKey, TUser}"/>.
    /// </summary>
    /// <typeparam name="TEntityPrimaryKey">The entity's key type</typeparam>
    /// <typeparam name="TUserPrimaryKey">The user's primary key type</typeparam>
    /// <typeparam name="TUser">Type of the user</typeparam>
    public abstract class AuditedEntity<TEntityPrimaryKey, TUserPrimaryKey, TUser> : 
        AuditedEntity<TEntityPrimaryKey, TUserPrimaryKey>, 
        ICreationAudited<TEntityPrimaryKey, TUserPrimaryKey, TUser>, 
        IModificationAudited<TEntityPrimaryKey, TUserPrimaryKey, TUser>
        where TUser : IEntity<TUserPrimaryKey>, IUser<TUserPrimaryKey>
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