using System;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using MediatR;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Events
{
    public abstract class Event : INotification
    {
        public DateTime Timestamp { get; }

        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }

    public abstract class Event<TEntity, TPrimaryKey> : Event
        where TEntity : IEntity<TPrimaryKey>
    {

    }

    public abstract class Event<TEntity> : Event<TEntity, int>
        where TEntity : IEntity<int>
    {

    }
}