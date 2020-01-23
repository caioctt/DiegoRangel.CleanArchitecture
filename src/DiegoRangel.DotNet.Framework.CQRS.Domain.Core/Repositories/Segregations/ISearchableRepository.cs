using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.PagedSearch;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories.Segregations
{
    public interface ISearchableRepository<TEntity, in TPrimaryKey> : IRepository
        where TEntity : IEntity<TPrimaryKey>
    {
        Task<List<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate);
        Task<PagedSearchList<TEntity>> SearchPagedAsync(Expression<Func<TEntity, bool>> predicate, int currentPage, int pageSize);
    }

    public interface ISearchableRepository<TEntity> : ISearchableRepository<TEntity, int>
        where TEntity : IEntity<int>
    {

    }
}