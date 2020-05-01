using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.MediatR;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Events
{
    public interface IEventWithId<TKey> : IEvent, IMustHaveId<TKey>
    {

    }
    public interface IEventWithId : IEventWithId<int>
    {

    }
}