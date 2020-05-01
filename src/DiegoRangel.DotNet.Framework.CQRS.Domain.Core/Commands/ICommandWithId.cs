using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.MediatR;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Commands
{
    public interface ICommandWithId<TKey, out TResponse> : ICommand<TResponse>, IMustHaveId<TKey>
    {

    }
    public interface ICommandWithId<TKey> : ICommand, IMustHaveId<TKey>
    {

    }
    public interface ICommandWithId : ICommandWithId<int>
    {

    }
}