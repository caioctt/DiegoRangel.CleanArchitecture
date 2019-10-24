using System.Collections.Generic;
using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.Data.EFCore.Repositories
{
    public abstract class WriteOnlyRepository<TEntity, TEntityKey> : Repository<TEntity, TEntityKey>, IWriteOnlyRepository<TEntity, TEntityKey>
        where TEntityKey : struct
        where TEntity : Entity<TEntityKey>
    {
        protected WriteOnlyRepository(DbContext context) : base(context)
        {
        }

        public virtual Task AddAsync(TEntity entity)
        {
            return DbSet.AddAsync(entity);
        }

        public virtual Task AddAsync(IList<TEntity> entities)
        {
            return DbSet.AddRangeAsync(entities);
        }

        public virtual Task UpdateAsync(TEntity entity)
        {
            DbSet.Update(entity);
            return Task.CompletedTask;
        }

        public virtual Task UpdateAsync(IList<TEntity> entities)
        {
            DbSet.UpdateRange(entities);
            return Task.CompletedTask;
        }

        public virtual async Task DeleteAsync(TEntityKey id)
        {
            var obj = await DbSet.FindAsync(id);
            DbSet.Remove(obj);
        }

        public virtual Task DeleteAsync(TEntity entity)
        {
            DbSet.Remove(entity);
            return Task.CompletedTask;
        }

        public virtual Task DeleteAsync(IList<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
            return Task.CompletedTask;
        }
    }

    public abstract class WriteOnlyRepository<TEntity> : WriteOnlyRepository<TEntity, int>
        where TEntity : Entity<int>
    {
        protected WriteOnlyRepository(DbContext context) : base(context)
        {
        }
    }
}