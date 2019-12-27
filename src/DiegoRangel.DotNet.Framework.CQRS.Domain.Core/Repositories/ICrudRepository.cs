using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.PagedSearch;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories
{
    public interface ICrudRepository<TEntity, in TPrimaryKey> : IRepository
        where TPrimaryKey : struct
        where TEntity : IEntity<TPrimaryKey>
    {
        Task<TEntity> FindByIdAsync(TPrimaryKey id);
        Task<List<TEntity>> FindAllAsync();
        Task<PagedSearchList<TEntity>> FindAllPagedAsync(int currentPage, int pageSize);
        Task<List<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate);
        Task<PagedSearchList<TEntity>> SearchPagedAsync(Expression<Func<TEntity, bool>> predicate, int currentPage, int pageSize);
        Task AddAsync(TEntity entity);
        Task AddAsync(IList<TEntity> entities);
        Task UpdateAsync(TEntity entity);
        Task UpdateAsync(IList<TEntity> entities);
        Task DeleteAsync(TPrimaryKey id);
        Task DeleteAsync(TEntity entity);
        Task DeleteAsync(IList<TEntity> entities);
    }

    public interface ICrudRepository<TEntity> : ICrudRepository<TEntity, int>
        where TEntity : IEntity<int>
    {
        
    }
}