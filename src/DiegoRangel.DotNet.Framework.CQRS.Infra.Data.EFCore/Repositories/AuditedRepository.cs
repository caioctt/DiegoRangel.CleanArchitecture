using System.Linq;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories.Agregations;
using Microsoft.EntityFrameworkCore;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.Data.EFCore.Repositories
{
    public abstract class AuditedRepository<TEntity, TEntityKey, TUserKey> :
        CreationAuditedRepository<TEntity, TEntityKey, TUserKey>,
        IAuditedRepository<TEntity, TEntityKey, TUserKey>
        where TEntityKey : struct
        where TUserKey : struct
        where TEntity : AuditedEntity<TEntityKey, TUserKey>, IAudited<TEntityKey, TUserKey>
    {
        protected override IQueryable<TEntity> Query => DbSet
            .OrderByDescending(x => x.CreationTime)
            .ThenByDescending(x => x.LastModificationTime);

        protected AuditedRepository(DbContext context) : base(context)
        {
            
        }
    }

    public abstract class AuditedRepository<TEntity> : 
        AuditedRepository<TEntity, int, int>
        where TEntity : AuditedEntity<int, int>, IAudited<int, int>
    {
        protected AuditedRepository(DbContext context) : base(context)
        {
        }
    }
}