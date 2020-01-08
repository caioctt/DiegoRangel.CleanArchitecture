using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Commands;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Mapper
{
    public static class MapperHelper
    {
        public static List<Tuple<Type, Type>> GetViewModelMappings(params Assembly[] assemblies)
        {
            var mappings = new List<Tuple<Type, Type>>();
            var types = assemblies.SelectMany(x => x.GetTypes()).ToList();

            var viewModels = types
                .Where(x => x.IsClass && x.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IViewModel<>))
                                .SelectMany(i => i.GetGenericArguments()).Any());

            foreach (var viewModel in viewModels)
            {
                var viewModelInterface = viewModel.GetInterfaces()
                    .FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IViewModel<>));

                var classType = viewModelInterface?.GenericTypeArguments.First();

                mappings.Add(new Tuple<Type, Type>(classType, viewModel));
                mappings.Add(new Tuple<Type, Type>(viewModel, classType));

                var commands = types
                    .Where(x =>
                        x.IsClass
                        && !x.IsAbstract
                        && x.GetInterfaces().Any(i =>
                            i.IsGenericType
                            && i.GetGenericTypeDefinition() == typeof(ICommandMapped<>)
                            && i.GetGenericArguments().FirstOrDefault() == classType)
                    ).Select(x => x).ToList();

                foreach (var command in commands)
                {
                    mappings.Add(new Tuple<Type, Type>(viewModel, command));
                }
            }

            return mappings;
        }
    }
}