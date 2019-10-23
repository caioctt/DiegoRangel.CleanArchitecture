using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Session;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing
{

    /// <summary>
    /// A shortcut of <see cref="IAudited{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    public interface IAudited : IAudited<int>
    {

    }

    /// <summary>
    /// This interface is implemented by entities which must be audited.
    /// Related properties automatically set when saving/updating <see cref="Entity"/> objects.
    /// </summary>
    public interface IAudited<TUserPrimaryKey> : ICreationAudited<TUserPrimaryKey>, IModificationAudited<TUserPrimaryKey>
    {

    }

    /// <summary>
    /// Adds navigation properties to <see cref="IAudited{TUser,TUserPrimaryKey}"/> interface for user.
    /// </summary>
    /// <typeparam name="TUser">Type of the user</typeparam>
    /// <typeparam name="TUserPrimaryKey">The user's primary key type</typeparam>
    public interface IAudited<TUser, TUserPrimaryKey> : IAudited<TUserPrimaryKey>, ICreationAudited<TUser, TUserPrimaryKey>, IModificationAudited<TUser, TUserPrimaryKey>
        where TUser : IEntity<TUserPrimaryKey>, IUser<TUserPrimaryKey>
    {

    }
}