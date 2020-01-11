using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories.Agregations;
using Microsoft.EntityFrameworkCore;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.Data.EFCore.Repositories
{
    public abstract class FullAuditedRepository<TEntity, TEntityKey, TUserKey> :
        AuditedRepository<TEntity, TEntityKey, TUserKey>,
        IFullAuditedRepository<TEntity, TEntityKey, TUserKey>
        where TEntityKey : struct
        where TUserKey : struct
        where TEntity : FullAuditedEntity<TEntityKey, TUserKey>
    {
        protected override IQueryable<TEntity> Query => DbSet
            .Where(x => !x.IsDeleted)
            .OrderByDescending(x => x.CreationTime)
            .ThenByDescending(x => x.LastModificationTime);

        protected FullAuditedRepository(DbContext context) : base(context)
        {

        }

        public virtual async Task MoveToTrashAsync(TEntityKey id)
        {
            var entity = await DbSet.FindAsync(id);
            await MoveToTrashAsync(entity);
        }

        public virtual Task MoveToTrashAsync(TEntity entity)
        {
            entity.MoveToTrash();
            DbSet.Update(entity);

            return Task.CompletedTask;
        }

        public virtual async Task MoveToTrashAsync(IList<TEntity> entities)
        {
            foreach (var entity in entities)
                await MoveToTrashAsync(entity);
        }
    }

    public abstract class FullAuditedRepository<TEntity> : 
        FullAuditedRepository<TEntity, int, int>
        where TEntity : FullAuditedEntity<int, int>
    {
        protected FullAuditedRepository(DbContext context) : base(context)
        {

        }
    }
}