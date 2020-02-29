using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.MediatR;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Events
{
    public interface IEventMapped<TEntity, TKey> : IEvent
        where TEntity : IEntity<TKey>
    {

    }
    public interface IEventMapped<TEntity> : IEventMapped<TEntity, int>
        where TEntity : IEntity<int>
    {

    }

    public interface IEventWithId<TKey> : IEvent, IMustHaveId<TKey>
    {

    }
    public interface IEventWithId : IEventWithId<int>
    {

    }

    public interface IEventMappedWithId<TEntity, TKey> : IEventMapped<TEntity, TKey>, IEventWithId<TKey>
        where TEntity : IEntity<TKey>
    {

    }
    public interface IEventMappedWithId<TEntity> : IEventMappedWithId<TEntity, int>
        where TEntity : IEntity<int>
    {

    }
}