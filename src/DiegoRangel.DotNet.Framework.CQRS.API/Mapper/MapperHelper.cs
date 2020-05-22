using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Commands;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.AutoMapper;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Mapper
{
    public static class MapperHelper
    {
        public static void ApplyViewModelMappings(this Profile profile, params Assembly[] assemblies)
        {
            var types = assemblies.SelectMany(x => x.GetTypes()).ToList();

            var viewModels = types
                .Where(x => x.IsClass && x.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IViewModel<>))
                                .SelectMany(i => i.GetGenericArguments()).Any());

            foreach (var viewModel in viewModels)
            {
                var viewModelInterface = viewModel.GetInterfaces()
                    .FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IViewModel<>));

                var classType = viewModelInterface?.GenericTypeArguments.First();

                profile.CreateMap(classType, viewModel).ReverseMap();

                var commands = types
                    .Where(x =>
                        x.IsClass
                        && !x.IsAbstract
                        && x.GetInterfaces().Any(i =>
                            i.IsGenericType
                            && (i.GetGenericTypeDefinition() == typeof(ICommandMapped<,>) 
                                || i.GetGenericTypeDefinition() == typeof(ICommandMapped<,,>))
                            && i.GetGenericArguments().FirstOrDefault() == classType)
                    ).Select(x => x).ToList();

                foreach (var command in commands)
                {
                    profile.CreateMap(viewModel, command);
                }
            }
        }

        public static void ApplyAutoMappings(this Profile profile, params Assembly[] assemblies)
        {
            var types = assemblies
                .SelectMany(x => x.GetTypes())
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod("Mapping")
                                 ?? type.GetInterface("IMapFrom`1")?.GetMethod("Mapping");

                methodInfo?.Invoke(instance, new object[] { profile });
            }
        }
    }
}