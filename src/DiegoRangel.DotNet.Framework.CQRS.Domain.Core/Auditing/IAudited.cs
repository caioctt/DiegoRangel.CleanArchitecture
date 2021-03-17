using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Session;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing
{

    /// <summary>
    /// A shortcut of <see cref="IAudited{TEntityPrimaryKey, TUserKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    public interface IAudited : IAudited<int, int>
    {

    }

    /// <summary>
    /// This interface is implemented by entities which must be audited.
    /// Related properties automatically set when saving/updating <see cref="Entity"/> objects.
    /// </summary>
    /// <typeparam name="TEntityPrimaryKey">The entity's key type</typeparam>
    /// <typeparam name="TUserKey">The user's primary key type</typeparam>
    public interface IAudited<TEntityPrimaryKey, TUserKey> : 
        ICreationAudited<TEntityPrimaryKey, TUserKey>, 
        IModificationAudited<TEntityPrimaryKey, TUserKey>
    {

    }

    /// <summary>
    /// Adds navigation properties to <see cref="IAudited{TUser,TUserKey}"/> interface for user.
    /// </summary>
    /// <typeparam name="TEntityPrimaryKey">The entity's key type</typeparam>
    /// <typeparam name="TUserKey">The user's primary key type</typeparam>
    /// <typeparam name="TUser">Type of the user</typeparam>
    public interface IAudited<TEntityPrimaryKey, TUserKey, TUser> : 
        IAudited<TEntityPrimaryKey, TUserKey>, 
        ICreationAudited<TEntityPrimaryKey, TUserKey, TUser>, 
        IModificationAudited<TEntityPrimaryKey, TUserKey, TUser>
        where TUser : IEntity<TUserKey>, IUser<TUserKey>
    {

    }
}