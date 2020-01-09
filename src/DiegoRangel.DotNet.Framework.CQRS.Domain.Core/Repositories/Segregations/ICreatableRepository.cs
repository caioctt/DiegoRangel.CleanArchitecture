using System.Collections.Generic;
using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories.Segregations
{
    public interface ICreatableRepository<TEntity, in TPrimaryKey> : IRepository
        where TPrimaryKey : struct
        where TEntity : IEntity<TPrimaryKey>
    {
        Task AddAsync(TEntity entity);
        Task AddAsync(IList<TEntity> entities);
    }

    public interface ICreatableRepository<TEntity> : ICreatableRepository<TEntity, int>
        where TEntity : IEntity<int>
    {

    }
}