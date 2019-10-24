using System;
using System.Collections.Generic;
using System.Linq;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Commands;

namespace DiegoRangel.DotNet.Framework.CQRS.API.AutoMapperSetup
{
    public static class MapperHelper
    {
        public static List<Tuple<Type, Type>> GetViewModelMappings()
        {
            var mappings = new List<Tuple<Type, Type>>();
            var types = GetAllAssemblies();

            var viewModels = types
                .Where(x => x.IsClass && x.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IViewModel<>))
                                .SelectMany(i => i.GetGenericArguments()).Any());

            foreach (var viewModel in viewModels)
            {
                var viewModelInterface = viewModel.GetInterfaces()
                    .FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IViewModel<>));

                var classe = viewModelInterface?.GenericTypeArguments.First();

                mappings.Add(new Tuple<Type, Type>(classe, viewModel));
                mappings.Add(new Tuple<Type, Type>(viewModel, classe));

                var comandos = types
                    .Where(x =>
                        x.IsClass
                        && !x.IsAbstract
                        && x.GetInterfaces().Any(i =>
                            i.IsGenericType
                            && i.GetGenericTypeDefinition() == typeof(ICommandMapped<>)
                            && i.GetGenericArguments().FirstOrDefault() == classe)
                    ).Select(x => x).ToList();

                foreach (var comando in comandos)
                {
                    mappings.Add(new Tuple<Type, Type>(viewModel, comando));
                }
            }

            return mappings;
        }

        private static IList<Type> GetAllAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).ToList();
        }
    }
}