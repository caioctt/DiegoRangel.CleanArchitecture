using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories.Segregations;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories.Agregations
{
    public interface IDeletionAuditedRepository<TEntity, in TEntityKey, TUserKey> : 
        ICrudRepository<TEntity, TEntityKey>,
        ISoftDeletableRepository<TEntity, TEntityKey>
        where TEntityKey : struct
        where TUserKey : struct
        where TEntity : IDeletionAudited<TEntityKey, TUserKey>
    {

    }

    public interface IDeletionAuditedRepository<TEntity> : IDeletionAuditedRepository<TEntity, int, int>
        where TEntity : IDeletionAudited<int, int>
    {

    }
}