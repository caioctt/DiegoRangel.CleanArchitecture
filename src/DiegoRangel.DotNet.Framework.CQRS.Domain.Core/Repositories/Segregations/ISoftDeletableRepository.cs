using System.Collections.Generic;
using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories.Segregations
{
    public interface ISoftDeletableRepository<TEntity, in TPrimaryKey> : IRepository
        where TPrimaryKey : struct
        where TEntity : IEntity<TPrimaryKey>, ISoftDelete
    {
        Task MoveToTrashAsync(TPrimaryKey id);
        Task MoveToTrashAsync(TEntity entity);
        Task MoveToTrashAsync(IList<TEntity> entities);
    }

    public interface ISoftDeletableRepository<TEntity> : ISoftDeletableRepository<TEntity, int>
        where TEntity : IEntity<int>, ISoftDelete
    {

    }
}