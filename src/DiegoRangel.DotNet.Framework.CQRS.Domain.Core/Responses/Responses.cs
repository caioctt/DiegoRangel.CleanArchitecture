using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.MediatR;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Responses
{
    public class Ok : IResponse
    {
        public object Result { get; }
        public bool IsInvalid => false;

        public Ok(object result)
        {
            Result = result;
        }
    }

    public class NoContent : IResponse
    {
        public bool IsInvalid => false;
    }

    public class Fail : IResponse
    {
        public bool IsInvalid => true;
    }
}