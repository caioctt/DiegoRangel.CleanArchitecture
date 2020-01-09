using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories.Segregations;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories
{
    public interface ICrudRepository<TEntity, in TPrimaryKey> :
        ICreatableRepository<TEntity, TPrimaryKey>,
        IUpdatableRepository<TEntity, TPrimaryKey>,
        IDeletableRepository<TEntity, TPrimaryKey>,
        IFindableRepository<TEntity, TPrimaryKey>,
        ISearchableRepository<TEntity, TPrimaryKey>
        where TPrimaryKey : struct
        where TEntity : IEntity<TPrimaryKey>
    {
        
    }

    public interface ICrudRepository<TEntity> : ICrudRepository<TEntity, int>
        where TEntity : IEntity<int>
    {
        
    }
}