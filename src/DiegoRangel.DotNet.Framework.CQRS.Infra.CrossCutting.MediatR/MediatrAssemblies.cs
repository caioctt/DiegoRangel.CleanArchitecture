using System.Reflection;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.MediatR
{
    public class MediatrAssemblies
    {
        public readonly Assembly[] Assemblies;

        public MediatrAssemblies(Assembly[] assemblies)
        {
            Assemblies = assemblies;
        }
    }
}