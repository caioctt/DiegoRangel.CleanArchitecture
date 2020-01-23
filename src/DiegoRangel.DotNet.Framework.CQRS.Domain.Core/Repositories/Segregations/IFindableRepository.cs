using System.Collections.Generic;
using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.PagedSearch;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories.Segregations
{
    public interface IFindableRepository<TEntity, in TPrimaryKey> : IRepository
        where TEntity : IEntity<TPrimaryKey>
    {
        Task<TEntity> FindByIdAsync(TPrimaryKey id);
        Task<List<TEntity>> FindAllAsync();
        Task<PagedSearchList<TEntity>> FindAllPagedAsync(int currentPage, int pageSize);
    }

    public interface IFindableRepository<TEntity> : IFindableRepository<TEntity, int>
        where TEntity : IEntity<int>
    {

    }
}