using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.AutoMapper;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Mapper
{
    public interface IViewModel<T> : IMapFrom<T>
        where T : class
    {

    }
}
