using MediatR;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.MediatR
{
    public interface ICommand : IRequest
    {
        
    }

    public interface ICommand<out TResponse> : IRequest<TResponse>
    {

    }
}