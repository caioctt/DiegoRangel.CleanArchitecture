using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.Data.EFCore.Repositories
{
    public abstract class CrudSoftDeletableRepository<TEntity, TEntityKey> : CrudRepository<TEntity, TEntityKey>, ICrudSoftDeletableRepository<TEntity, TEntityKey>
        where TEntityKey : struct
        where TEntity : Entity<TEntityKey>, ISoftDelete
    {
        protected override IQueryable<TEntity> Query => DbSet.Where(x => !x.IsDeleted);

        protected CrudSoftDeletableRepository(DbContext context) : base(context)
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

    public abstract class CrudSoftDeletableRepository<TEntity> : CrudSoftDeletableRepository<TEntity, int>
        where TEntity : Entity<int>, ISoftDelete
    {
        protected CrudSoftDeletableRepository(DbContext context) : base(context)
        {
        }
    }
}