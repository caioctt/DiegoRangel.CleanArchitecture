using System.Collections.Generic;
using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories.Segregations
{
    public interface IDeletableRepository<TEntity, in TPrimaryKey> : IRepository
        where TPrimaryKey : struct
        where TEntity : IEntity<TPrimaryKey>
    {
        Task DeleteAsync(TPrimaryKey id);
        Task DeleteAsync(TEntity entity);
        Task DeleteAsync(IList<TEntity> entities);
    }

    public interface IDeletableRepository<TEntity> : IDeletableRepository<TEntity, int>
        where TEntity : IEntity<int>
    {

    }
}