using MediatR;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.MediatR;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Handlers
{
    public interface IEventHandler<in TEvent> : INotificationHandler<TEvent>
        where TEvent : IEvent
    {
        
    }
}