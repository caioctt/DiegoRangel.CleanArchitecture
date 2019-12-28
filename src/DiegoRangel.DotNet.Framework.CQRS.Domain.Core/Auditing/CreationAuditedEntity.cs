using System;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Session;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing
{
    /// <summary>
    /// A shortcut of <see cref="CreationAuditedEntity{TEntityPrimaryKey, TUserPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    public abstract class CreationAuditedEntity : CreationAuditedEntity<int, int>
    {

    }

    /// <summary>
    /// This class can be used to simplify implementing <see cref="ICreationAudited"/>.
    /// </summary>
    /// <typeparam name="TEntityPrimaryKey">The entity's key type</typeparam>
    /// <typeparam name="TUserPrimaryKey">The user's primary key type</typeparam>
    public abstract class CreationAuditedEntity<TEntityPrimaryKey, TUserPrimaryKey> : Entity<TEntityPrimaryKey>, ICreationAudited<TEntityPrimaryKey, TUserPrimaryKey>
    {
        /// <summary>
        /// Creation time of this entity.
        /// </summary>
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// Creator of this entity.
        /// </summary>
        public virtual TUserPrimaryKey CreatorUserId { get; set; }
    }

    /// <summary>
    /// This class can be used to simplify implementing <see cref="ICreationAudited{TEntityPrimaryKey, TUserPrimaryKey, TUser}"/>.
    /// </summary>
    /// <typeparam name="TEntityPrimaryKey">The entity's key type</typeparam>
    /// <typeparam name="TUserPrimaryKey">The user's primary key type</typeparam>
    /// <typeparam name="TUser">Type of the user</typeparam>
    public abstract class CreationAuditedEntity<TEntityPrimaryKey, TUserPrimaryKey, TUser> : 
        CreationAuditedEntity<TEntityPrimaryKey, TUserPrimaryKey>, 
        ICreationAudited<TEntityPrimaryKey, TUserPrimaryKey, TUser>
        where TUser : IEntity<TUserPrimaryKey>, IUser<TUserPrimaryKey>
    {
        /// <summary>
        /// Reference to the creator user of this entity.
        /// </summary>
        public virtual TUser CreatorUser { get; set; }
    }
}