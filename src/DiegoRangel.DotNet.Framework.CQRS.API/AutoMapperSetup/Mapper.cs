using AutoMapper;

namespace DiegoRangel.DotNet.Framework.CQRS.API.AutoMapperSetup
{
    public class Mapeador : Domain.Core.Interfaces.IMapper
    {
        private readonly IMapper _mapper;

        public Mapeador(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TDestination Map<TDestination>(object source)
        {
            return _mapper.Map<TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TSource, TDestination>(source);
        }

        public void Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            _mapper.Map(source, destination);
        }
    }
}