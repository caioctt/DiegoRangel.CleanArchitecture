using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.PagedSearch;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories
{
    public interface IReadOnlyRepository<TEntity, in TPrimaryKey> : IRepository
        where TPrimaryKey : struct
        where TEntity : IEntity<TPrimaryKey>
    {
        Task<TEntity> FindByIdAsync(TPrimaryKey id);

        Task<List<TEntity>> FindAllAsync();
        Task<PagedSearchList<TEntity>> FindAllPagedAsync(int currentPage, int pageSize);

        Task<List<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> condition);
        Task<PagedSearchList<TEntity>> SearchPagedAsync(Expression<Func<TEntity, bool>> condition, int currentPage, int pageSize);
    }

    public interface IReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity, int>
        where TEntity : IEntity<int>
    {
        
    }
}