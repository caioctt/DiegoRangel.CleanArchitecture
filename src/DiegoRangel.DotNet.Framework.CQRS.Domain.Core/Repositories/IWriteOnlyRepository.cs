using System.Collections.Generic;
using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories
{
    public interface IWriteOnlyRepository<TEntity, in TPrimaryKey> : IRepository
        where TPrimaryKey : struct
        where TEntity : IEntity<TPrimaryKey>
    {
        Task AddAsync(TEntity entity);
        Task AddAsync(IList<TEntity> entities);
        Task UpdateAsync(TEntity entity);
        Task UpdateAsync(IList<TEntity> entities);
        Task DeleteAsync(TPrimaryKey id);
        Task DeleteAsync(TEntity entity);
        Task DeleteAsync(IList<TEntity> entities);
    }

    public interface IWriteOnlyRepository<TEntity> : IWriteOnlyRepository<TEntity, int>
        where TEntity : IEntity<int>
    {
        
    }
}