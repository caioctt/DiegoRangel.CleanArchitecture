using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories.Agregations
{
    public interface IModificationAuditedRepository<TEntity, in TEntityKey, TUserKey> : ICrudRepository<TEntity, TEntityKey>
        where TEntityKey : struct
        where TUserKey : struct
        where TEntity : IModificationAudited<TEntityKey, TUserKey>
    {

    }

    public interface IModificationAuditedRepository<TEntity> : IModificationAuditedRepository<TEntity, int, int>
        where TEntity : IModificationAudited<int, int>
    {

    }
}