using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Interfaces
{
    public interface ICrudSoftDeletableRepository<TEntity, in TPrimaryKey> :
        IReadOnlyRepository<TEntity, TPrimaryKey>,
        IWriteOnlySoftDeletableRepository<TEntity, TPrimaryKey>
        where TPrimaryKey : struct
        where TEntity : IEntity<TPrimaryKey>, ISoftDelete
    {

    }

    public interface ICrudSoftDeletableRepository<TEntity> : ICrudSoftDeletableRepository<TEntity, int>
        where TEntity : IEntity<int>, ISoftDelete
    {

    }
}