using System.Collections.Generic;
using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories.Segregations
{
    public interface IUpdatableRepository<TEntity, in TPrimaryKey> : IRepository
        where TEntity : IEntity<TPrimaryKey>
    {
        Task UpdateAsync(TEntity entity);
        Task UpdateAsync(IList<TEntity> entities);
    }

    public interface IUpdatableRepository<TEntity> : IUpdatableRepository<TEntity, int>
        where TEntity : IEntity<int>
    {

    }
}