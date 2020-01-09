using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories.Agregations
{
    public interface ICreationAuditedRepository<TEntity, in TEntityKey, TUserKey> : ICrudRepository<TEntity, TEntityKey>
        where TEntityKey : struct
        where TUserKey : struct
        where TEntity : ICreationAudited<TEntityKey, TUserKey>
    {

    }

    public interface ICreationAuditedRepository<TEntity> : ICreationAuditedRepository<TEntity, int, int>
        where TEntity : ICreationAudited<int, int>
    {

    }
}