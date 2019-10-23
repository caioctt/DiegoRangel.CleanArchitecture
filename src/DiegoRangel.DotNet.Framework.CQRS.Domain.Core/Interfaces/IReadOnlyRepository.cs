using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Interfaces
{
    public interface IReadOnlyRepository<TEntity, in TPrimaryKey> : IRepository
        where TPrimaryKey : struct
        where TEntity : IEntity<TPrimaryKey>
    {
        Task<TEntity> FindById(TPrimaryKey id);
        Task<List<TEntity>> FindAll();
        Task<List<TEntity>> Search(Expression<Func<TEntity, bool>> condition);
    }

    public interface IReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity, int>
        where TEntity : IEntity<int>
    {
        
    }
}