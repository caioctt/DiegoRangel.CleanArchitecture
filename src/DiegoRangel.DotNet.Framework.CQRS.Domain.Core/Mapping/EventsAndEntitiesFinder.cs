using System;
using System.Collections.Generic;
using System.Linq;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Events;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Mapping
{
    public static class EventsAndEntitiesFinder
    {
        public static List<Tuple<Type, Type>> GetMappingsFrom(Type assemblyScanner)
        {
            var mappings = new List<Tuple<Type, Type>>();
            var types = AppDomain.CurrentDomain.Load(assemblyScanner.Namespace).GetTypes().ToList();

            var events = types
                .Where(x =>
                    x.IsClass 
                    && !x.IsAbstract 
                    && x.IsSubclassOf(typeof(Event))
                ).Select(x => x).ToList();

            foreach (var x in events)
            {
                var entity = x.BaseType?.GenericTypeArguments.FirstOrDefault();

                if (entity == null)
                    continue;

                mappings.Add(new Tuple<Type, Type>(entity, x));
            }

            return mappings;
        }
    }
}