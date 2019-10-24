using System.Collections.Generic;
using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.Data.EFCore.Repositories
{
    public abstract class WriteOnlySoftDeletableRepository<TEntity, TEntityKey> : WriteOnlyRepository<TEntity, TEntityKey>, IWriteOnlySoftDeletableRepository<TEntity, TEntityKey>
        where TEntityKey : struct
        where TEntity : Entity<TEntityKey>, ISoftDelete
    {
        protected WriteOnlySoftDeletableRepository(DbContext context) : base(context)
        {
        }

        public virtual async Task MoveToTrashAsync(TEntityKey id)
        {
            var obj = await DbSet.FindAsync(id);
            obj.IsDeleted = true;
            DbSet.Update(obj);
        }

        public virtual Task MoveToTrashAsync(TEntity entity)
        {
            entity.IsDeleted = true;
            DbSet.Update(entity);

            return Task.CompletedTask;
        }

        public virtual Task MoveToTrashAsync(IList<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.IsDeleted = true;
                DbSet.Update(entity);
            }

            return Task.CompletedTask;
        }
    }

    public abstract class WriteOnlySoftDeletableRepository<TEntity> : WriteOnlySoftDeletableRepository<TEntity, int>
        where TEntity : Entity<int>, ISoftDelete
    {
        protected WriteOnlySoftDeletableRepository(DbContext context) : base(context)
        {
        }
    }
}