using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.MediatR;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.AutoMapper;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Commands
{
    public interface ICommandMapped<TEntity, TKey, out TResponse> : ICommand<TResponse>, IMapFrom<TEntity>
        where TEntity : IEntity<TKey>
    {

    }

    public interface ICommandMapped<TEntity, TKey> : ICommand, IMapFrom<TEntity>
        where TEntity : IEntity<TKey>
    {

    }

    public interface ICommandMapped<TEntity> : ICommandMapped<TEntity, int>
        where TEntity : IEntity<int>
    {

    }
}