using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories.Agregations
{
    public interface IModificationAuditedRepository<TEntity, in TEntityKey, TUserKey> : ICrudRepository<TEntity, TEntityKey>
        where TEntity : IModificationAudited<TEntityKey, TUserKey>
        where TUserKey : struct
    {

    }

    public interface IModificationAuditedRepository<TEntity> : IModificationAuditedRepository<TEntity, int, int>
        where TEntity : IModificationAudited<int, int>
    {

    }
}