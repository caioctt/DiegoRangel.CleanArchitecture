using System.Reflection;
using AutoMapper;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Mapper
{
    /// <summary>
    /// A base Profile class that is used to automatically map all classes which implements IMapFrom interface. 
    /// </summary>
    public abstract class ProfileBase : Profile
    {
        protected ProfileBase()
        {
            Apply();
        }

        protected void Apply()
        {
            this.ApplyAutoMappings(GetAssembliesForAutomation());
        }

        /// <summary>
        /// Get all assemblies that are going to be used for automation profiles.
        /// </summary>
        /// <returns></returns>
        protected abstract Assembly[] GetAssembliesForAutomation();
    }
}