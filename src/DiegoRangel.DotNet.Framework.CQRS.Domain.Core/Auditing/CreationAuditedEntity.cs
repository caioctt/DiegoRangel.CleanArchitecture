using System;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Session;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing
{
    /// <summary>
    /// A shortcut of <see cref="CreationAuditedEntity{TEntityPrimaryKey, TUserKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    public abstract class CreationAuditedEntity : CreationAuditedEntity<int, int>
    {

    }

    /// <summary>
    /// This class can be used to simplify implementing <see cref="ICreationAudited"/>.
    /// </summary>
    /// <typeparam name="TEntityPrimaryKey">The entity's key type</typeparam>
    /// <typeparam name="TUserKey">The user's primary key type</typeparam>
    public abstract class CreationAuditedEntity<TEntityPrimaryKey, TUserKey> : Entity<TEntityPrimaryKey>, ICreationAudited<TEntityPrimaryKey, TUserKey>
    {
        /// <summary>
        /// Creation time of this entity.
        /// </summary>
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// Creator of this entity.
        /// </summary>
        public virtual TUserKey CreatorUserId { get; set; }
    }

    /// <summary>
    /// This class can be used to simplify implementing <see cref="ICreationAudited{TEntityPrimaryKey, TUserKey, TUser}"/>.
    /// </summary>
    /// <typeparam name="TEntityPrimaryKey">The entity's key type</typeparam>
    /// <typeparam name="TUserKey">The user's primary key type</typeparam>
    /// <typeparam name="TUser">Type of the user</typeparam>
    public abstract class CreationAuditedEntity<TEntityPrimaryKey, TUserKey, TUser> : 
        CreationAuditedEntity<TEntityPrimaryKey, TUserKey>, 
        ICreationAudited<TEntityPrimaryKey, TUserKey, TUser>
        where TUser : IEntity<TUserKey>, IUser<TUserKey>
    {
        /// <summary>
        /// Reference to the creator user of this entity.
        /// </summary>
        public virtual TUser CreatorUser { get; set; }
    }
}