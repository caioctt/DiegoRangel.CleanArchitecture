using System.Collections.Generic;
using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Interfaces
{
    public interface IWriteOnlyRepository<TEntity, in TPrimaryKey> : IRepository
        where TPrimaryKey : struct
        where TEntity : IEntity<TPrimaryKey>
    {
        Task Add(TEntity entity);
        Task Add(IList<TEntity> entities);
        Task Update(TEntity entity);
        Task Update(IList<TEntity> entities);
        Task Delete(TPrimaryKey id);
        Task Delete(TEntity entity);
        Task Delete(IList<TEntity> entities);
    }

    public interface IWriteOnlyRepository<TEntity> : IWriteOnlyRepository<TEntity, int>
        where TEntity : IEntity<int>
    {
        
    }
}