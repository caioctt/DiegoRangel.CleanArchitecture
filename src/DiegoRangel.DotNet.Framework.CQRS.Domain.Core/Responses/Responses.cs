namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Responses
{
    public interface IResponse
    {
        bool IsInvalid { get; }
    }

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