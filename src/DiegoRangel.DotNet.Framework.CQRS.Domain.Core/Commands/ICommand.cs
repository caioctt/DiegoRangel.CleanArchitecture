using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.MediatR;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Commands
{
    public interface ICommandMapped<T, K> : ICommand
        where K : struct
        where T : IEntity<K>
    {

    }
    public interface ICommandMapped<T> : ICommandMapped<T, int>
        where T : IEntity<int>
    {

    }

    public interface ICommandWithId<K> : ICommand, IMustHaveId<K>
    {

    }
    public interface ICommandWithId : ICommandWithId<int>
    {

    }

    public interface ICommandMappedWithId<T, K> : ICommandMapped<T, K>, ICommandWithId<K>
        where K : struct
        where T : IEntity<K>
    {

    }
    public interface ICommandMappedWithId<T> : ICommandMappedWithId<T, int>
        where T : IEntity<int>
    {

    }
}