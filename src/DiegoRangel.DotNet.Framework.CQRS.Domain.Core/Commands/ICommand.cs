using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.MediatR;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Commands
{
    public interface ICommandMapped<TEntity, TKey> : ICommand
        where TEntity : IEntity<TKey>
    {

    }
    public interface ICommandMapped<TEntity> : ICommandMapped<TEntity, int>
        where TEntity : IEntity<int>
    {

    }

    public interface ICommandWithId<TKey> : ICommand, IMustHaveId<TKey>
    {

    }
    public interface ICommandWithId : ICommandWithId<int>
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