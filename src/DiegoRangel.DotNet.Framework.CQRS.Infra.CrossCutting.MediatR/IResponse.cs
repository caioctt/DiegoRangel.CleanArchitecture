namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.MediatR
{
    public interface IResponse
    {
        bool IsInvalid { get; }
    }
}