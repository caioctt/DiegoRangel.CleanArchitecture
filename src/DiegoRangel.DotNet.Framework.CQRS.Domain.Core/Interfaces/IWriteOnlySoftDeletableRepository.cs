using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Interfaces
{
    public interface IWriteOnlySoftDeletableRepository<TEntity, in TPrimaryKey> : IWriteOnlyRepository<TEntity, TPrimaryKey>
        where TPrimaryKey : struct
        where TEntity : IEntity<TPrimaryKey>, ISoftDelete
    {
        Task MoveToTrash(TPrimaryKey id);
        Task MoveToTrash(TEntity obj);
    }

    public interface IWriteOnlySoftDeletableRepository<TEntity> : IWriteOnlySoftDeletableRepository<TEntity, int>
        where TEntity : IEntity<int>, ISoftDelete
    {
        
    }
}