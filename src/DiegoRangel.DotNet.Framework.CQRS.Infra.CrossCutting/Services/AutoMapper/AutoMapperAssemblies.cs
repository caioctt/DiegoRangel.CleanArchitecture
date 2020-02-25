using System.Reflection;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.AutoMapper
{
    public class AutoMapperAssemblies
    {
        public readonly Assembly[] Assemblies;

        public AutoMapperAssemblies(Assembly[] assemblies)
        {
            Assemblies = assemblies;
        }
    }
}
