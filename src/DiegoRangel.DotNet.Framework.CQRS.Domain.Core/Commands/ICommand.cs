using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Responses;
using MediatR;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Commands
{
    public interface ICommand : IRequest<IResponse>
    {

    }

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