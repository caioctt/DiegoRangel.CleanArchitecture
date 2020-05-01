using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.MediatR;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Commands
{
    public interface ICommandMapped<TEntity, TKey, out TResponse> : ICommand<TResponse>
        where TEntity : IEntity<TKey>
    {

    }

    public interface ICommandMapped<TEntity, TKey> : ICommand
        where TEntity : IEntity<TKey>
    {

    }

    public interface ICommandMapped<TEntity> : ICommandMapped<TEntity, int>
        where TEntity : IEntity<int>
    {

    }
}