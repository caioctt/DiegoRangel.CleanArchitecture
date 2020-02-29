using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Events;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Mapping
{
    public static class EventsAndEntitiesFinder
    {
        public static List<Tuple<Type, Type>> GetMappingsFrom(params Assembly[] assemblies)
        {
            var mappings = new List<Tuple<Type, Type>>();
            var types = assemblies.SelectMany(x => x.GetTypes()).ToList();

            var events = types.Where(x =>
                x.IsClass && x.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEventMapped<,>))
                    .SelectMany(i => i.GetGenericArguments()).Any());

            foreach (var evt in events)
            {
                var eventInterface = evt.GetInterfaces().FirstOrDefault(x =>
                    x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEventMapped<,>));

                var entity = eventInterface?.GenericTypeArguments.First();

                mappings.Add(new Tuple<Type, Type>(evt, entity));
                mappings.Add(new Tuple<Type, Type>(entity, evt));
            }

            return mappings;
        }
    }
}