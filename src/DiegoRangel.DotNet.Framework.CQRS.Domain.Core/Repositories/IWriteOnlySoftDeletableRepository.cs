using System.Collections.Generic;
using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories
{
    public interface IWriteOnlySoftDeletableRepository<TEntity, in TPrimaryKey> : IWriteOnlyRepository<TEntity, TPrimaryKey>
        where TPrimaryKey : struct
        where TEntity : IEntity<TPrimaryKey>, ISoftDelete
    {
        Task MoveToTrashAsync(TPrimaryKey id);
        Task MoveToTrashAsync(TEntity entity);
        Task MoveToTrashAsync(IList<TEntity> entities);
    }

    public interface IWriteOnlySoftDeletableRepository<TEntity> : IWriteOnlySoftDeletableRepository<TEntity, int>
        where TEntity : IEntity<int>, ISoftDelete
    {
        
    }
}