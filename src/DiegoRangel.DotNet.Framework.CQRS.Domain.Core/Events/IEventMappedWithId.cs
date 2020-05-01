using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Events
{
    public interface IEventMappedWithId<TEntity, TKey> : IEventMapped<TEntity, TKey>, IEventWithId<TKey>
        where TEntity : IEntity<TKey>
    {

    }
    public interface IEventMappedWithId<TEntity> : IEventMappedWithId<TEntity, int>
        where TEntity : IEntity<int>
    {

    }
}