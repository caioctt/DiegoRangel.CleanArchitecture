using AutoMapper;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.AutoMapper
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType()).ReverseMap();
    }
}
