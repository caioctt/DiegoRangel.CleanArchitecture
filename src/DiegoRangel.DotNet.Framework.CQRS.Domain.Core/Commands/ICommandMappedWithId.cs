using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using MediatR;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Commands
{
    public interface ICommandMappedWithId<TEntity, TKey, out TResponse> : ICommandMapped<TEntity, TKey, TResponse>, ICommandWithId<TKey, TResponse>
        where TEntity : IEntity<TKey>
    {

    }
    public interface ICommandMappedWithId<TEntity, TKey> : ICommandMapped<TEntity, TKey>, ICommandWithId<TKey>
        where TEntity : IEntity<TKey>
    {

    }
    public interface ICommandMappedWithId<TEntity> : ICommandMappedWithId<TEntity, int>
        where TEntity : IEntity<int>
    {

    }
}