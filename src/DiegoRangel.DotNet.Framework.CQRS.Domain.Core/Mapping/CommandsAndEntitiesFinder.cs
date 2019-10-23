using System;
using System.Collections.Generic;
using System.Linq;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Commands;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Mapping
{
    public static class CommandsAndEntitiesFinder
    {
        public static List<Tuple<Type, Type>> GetMappingsFrom(string projectName)
        {
            var mappings = new List<Tuple<Type, Type>>();
            var types = AppDomain.CurrentDomain.Load(projectName).GetTypes().ToList();

            var commands = types.Where(x =>
                x.IsClass && x.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandMapped<,>))
                    .SelectMany(i => i.GetGenericArguments()).Any());

            foreach (var command in commands)
            {
                var commandInterface = command.GetInterfaces().FirstOrDefault(x =>
                    x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICommandMapped<,>));

                var entity = commandInterface?.GenericTypeArguments.First();

                mappings.Add(new Tuple<Type, Type>(command, entity));
                mappings.Add(new Tuple<Type, Type>(entity, command));
            }

            return mappings;
        }
    }
}