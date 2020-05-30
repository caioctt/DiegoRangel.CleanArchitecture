using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.MediatR;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.AutoMapper;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Events
{
    public interface IEventMapped<TEntity, TKey> : IEvent, IMapFrom<TEntity>
        where TEntity : IEntity<TKey>
    {

    }
    public interface IEventMapped<TEntity> : IEventMapped<TEntity, int>
        where TEntity : IEntity<int>
    {

    }
}