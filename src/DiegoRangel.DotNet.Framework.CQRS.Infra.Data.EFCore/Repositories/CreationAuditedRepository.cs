using System.Linq;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories.Agregations;
using Microsoft.EntityFrameworkCore;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.Data.EFCore.Repositories
{
    public abstract class CreationAuditedRepository<TEntity, TEntityKey, TUserKey> :
        CrudRepository<TEntity, TEntityKey>,
        ICreationAuditedRepository<TEntity, TEntityKey, TUserKey> 
        where TEntityKey : struct
        where TUserKey : struct
        where TEntity : CreationAuditedEntity<TEntityKey, TUserKey>
    {
        protected override IQueryable<TEntity> Query => DbSet.OrderByDescending(x => x.CreationTime);

        protected CreationAuditedRepository(DbContext context) : base(context)
        {
        }
    }

    public abstract class CreationAuditedRepository<TEntity> : 
        CreationAuditedRepository<TEntity, int, int>
        where TEntity : CreationAuditedEntity<int, int>
    {
        protected CreationAuditedRepository(DbContext context) : base(context)
        {
        }
    }
}