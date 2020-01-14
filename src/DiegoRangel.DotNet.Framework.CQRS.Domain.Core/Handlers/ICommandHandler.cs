using MediatR;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.MediatR;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Handlers
{
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, IResponse>
        where TCommand : ICommand
    {

    }
}