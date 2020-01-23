using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories.Agregations
{
    public interface IAuditedRepository<TEntity, in TEntityKey, TUserKey> : 
        ICreationAuditedRepository<TEntity, TEntityKey, TUserKey>,
        IModificationAuditedRepository<TEntity, TEntityKey, TUserKey>
        where TEntity : IAudited<TEntityKey, TUserKey>
    {

    }

    public interface IAuditedRepository<TEntity> : IAuditedRepository<TEntity, int, int>
        where TEntity : IAudited<int, int>
    {

    }
}