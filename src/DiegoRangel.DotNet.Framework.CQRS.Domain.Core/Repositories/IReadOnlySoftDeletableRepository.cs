using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories
{
    public interface IReadOnlySoftDeletableRepository<TEntity, in TPrimaryKey> : IReadOnlyRepository<TEntity, TPrimaryKey>
        where TPrimaryKey : struct
        where TEntity : IEntity<TPrimaryKey>, ISoftDelete
    {
        
    }

    public interface IReadOnlySoftDeletableRepository<TEntity> : IReadOnlySoftDeletableRepository<TEntity, int>
        where TEntity : IEntity<int>, ISoftDelete
    {

    }
}