using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories
{
    public interface ICrudRepository<TEntity, in TPrimaryKey> : 
        IReadOnlyRepository<TEntity, TPrimaryKey>, 
        IWriteOnlyRepository<TEntity, TPrimaryKey>
        where TPrimaryKey : struct
        where TEntity : IEntity<TPrimaryKey>
    {
        
    }

    public interface ICrudRepository<TEntity> : ICrudRepository<TEntity, int> 
        where TEntity : IEntity<int>
    {

    }
}