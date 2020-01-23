using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories.Agregations
{
    public interface IFullAuditedRepository<TEntity, in TEntityKey, TUserKey> :
        IAuditedRepository<TEntity, TEntityKey, TUserKey>,
        IDeletionAuditedRepository<TEntity, TEntityKey, TUserKey>
        where TEntity : IFullAudited<TEntityKey, TUserKey>
    {

    }

    public interface IFullAuditedRepository<TEntity> : IFullAuditedRepository<TEntity, int, int>
        where TEntity : IFullAudited<int, int>
    {

    }
}