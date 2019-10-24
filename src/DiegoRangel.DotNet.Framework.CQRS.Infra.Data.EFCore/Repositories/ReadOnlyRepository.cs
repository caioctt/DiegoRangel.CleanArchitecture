using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.PagedSearch;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.Data.EFCore.Repositories
{
    public abstract class ReadOnlyRepository<TEntity, TEntityKey> : Repository<TEntity, TEntityKey>, IReadOnlyRepository<TEntity, TEntityKey>
        where TEntityKey : struct
        where TEntity : Entity<TEntityKey>
    {
        protected ReadOnlyRepository(DbContext context) : base(context)
        {
        }

        public virtual Task<List<TEntity>> FindAllAsync()
        {
            return DbSet.ToListAsync();
        }

        public virtual async Task<PagedSearchList<TEntity>> FindAllPagedAsync(int currentPage, int pageSize)
        {
            var count = await DbSet.CountAsync();
            var data = await DbSet
                .Take(pageSize)
                .Skip((currentPage - 1) * pageSize)
                .ToListAsync();

            return new PagedSearchList<TEntity>
            {
                TotalResults = count,
                CurrentPage = currentPage,
                PageSize = pageSize,
                List = data
            };
        }

        public virtual Task<TEntity> FindByIdAsync(TEntityKey id)
        {
            return DbSet.FindAsync(id);
        }

        public virtual Task<List<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> condition)
        {
            return DbSet
                .Where(condition)
                .ToListAsync();
        }

        public virtual async Task<PagedSearchList<TEntity>> SearchPagedAsync(Expression<Func<TEntity, bool>> condition, int currentPage, int pageSize)
        {
            var query = DbSet.Where(condition);
            var count = await query.CountAsync();
            var data = await query
                .Take(pageSize)
                .Skip((currentPage - 1) * pageSize)
                .ToListAsync();

            return new PagedSearchList<TEntity>
            {
                TotalResults = count,
                CurrentPage = currentPage,
                PageSize = pageSize,
                List = data
            };
        }
    }

    public abstract class ReadOnlyRepository<TEntity> : ReadOnlyRepository<TEntity, int>
        where TEntity : Entity<int>
    {
        protected ReadOnlyRepository(DbContext context) : base(context)
        {
        }
    }
}