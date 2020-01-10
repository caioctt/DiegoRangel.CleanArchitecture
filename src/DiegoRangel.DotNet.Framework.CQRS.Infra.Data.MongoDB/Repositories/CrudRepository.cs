using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.PagedSearch;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories;
using DiegoRangel.DotNet.Framework.CQRS.Infra.Data.MongoDB.Context;
using MongoDB.Driver;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.Data.MongoDB.Repositories
{
    public abstract class CrudRepository<TEntity, TEntityKey> : 
        Repository<TEntity, TEntityKey>, 
        ICrudRepository<TEntity, TEntityKey>
        where TEntityKey : struct
        where TEntity : Entity<TEntityKey>
    {
        protected CrudRepository(IMongoContext context) : base(context)
        {
            
        }

        public virtual async Task<TEntity> FindByIdAsync(TEntityKey id)
        {
            var query = await QueryAsync(x => x.Id.Equals(id));
            return await query.FirstOrDefaultAsync();
        }

        public virtual async Task<List<TEntity>> FindAllAsync()
        {
            var query = await QueryAsync();
            return await query.ToListAsync();
        }

        public virtual async Task<PagedSearchList<TEntity>> FindAllPagedAsync(int currentPage, int pageSize)
        {
            var filter = BuildFindFilters();
            var counterQuery = await DbSet.CountDocumentsAsync(filter);
            var dataQuery = await QueryAsync(findOptions: new FindOptions<TEntity>
            {
                Skip = (currentPage - 1) * pageSize,
                Limit = pageSize
            });
            
            return new PagedSearchList<TEntity>
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalResults = counterQuery,
                List = await dataQuery.ToListAsync()
            };
        }

        public virtual async Task<List<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var query = await QueryAsync(predicate);
            return await query.ToListAsync();
        }

        public virtual async Task<PagedSearchList<TEntity>> SearchPagedAsync(Expression<Func<TEntity, bool>> predicate, int currentPage, int pageSize)
        {
            var filter = BuildFindFilters(predicate);
            var counterQuery = await DbSet.CountDocumentsAsync(filter);
            var dataQuery = await QueryAsync(findOptions: new FindOptions<TEntity>
            {
                Skip = (currentPage - 1) * pageSize,
                Limit = pageSize
            });

            return new PagedSearchList<TEntity>
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalResults = counterQuery,
                List = await dataQuery.ToListAsync()
            };
        }

        public virtual Task AddAsync(TEntity entity)
        {
            Context.AddCommand(() => DbSet.InsertOneAsync(entity));
            return Task.CompletedTask;
        }

        public virtual async Task AddAsync(IList<TEntity> entities)
        {
            foreach (var entity in entities)
                await AddAsync(entity);
        }

        public virtual Task UpdateAsync(TEntity entity)
        {
            Context.AddCommand(() => DbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq(x => x.Id, entity.Id), entity));
            return Task.CompletedTask;
        }

        public virtual async Task UpdateAsync(IList<TEntity> entities)
        {
            foreach (var entity in entities)
                await UpdateAsync(entity);
        }

        public virtual Task DeleteAsync(TEntityKey id)
        {
            Context.AddCommand(() => DbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq(x => x.Id, id)));
            return Task.CompletedTask;
        }

        public virtual Task DeleteAsync(TEntity entity)
        {
            return DeleteAsync(entity.Id);
        }

        public virtual async Task DeleteAsync(IList<TEntity> entities)
        {
            foreach (var entity in entities)
                await DeleteAsync(entity);
        }

        protected Task<IAsyncCursor<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> predicate = null, FindOptions<TEntity> findOptions = null)
        {
            return DbSet.FindAsync(BuildFindFilters(predicate), BuildFindOptions(findOptions));
        }
        protected FilterDefinition<TEntity> BuildFindFilters(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate == null
                ? BuildDefaultFilterDefinition()
                : Builders<TEntity>.Filter.And(
                    BuildDefaultFilterDefinition(),
                    Builders<TEntity>.Filter.Where(predicate));
        }
        protected FindOptions<TEntity> BuildFindOptions(FindOptions<TEntity> findOptions = null)
        {
            return findOptions ?? new FindOptions<TEntity> {Sort = BuildDefaultSortDefinition()};
        }
        
        protected virtual FilterDefinition<TEntity> BuildDefaultFilterDefinition()
        {
            return Builders<TEntity>.Filter.Empty;
        }
        protected virtual SortDefinition<TEntity> BuildDefaultSortDefinition()
        {
            return null;
        }
    }

    public abstract class CrudRepository<TEntity> : CrudRepository<TEntity, int>
        where TEntity : Entity<int>
    {
        protected CrudRepository(IMongoContext context) : base(context)
        {
        }
    }
}