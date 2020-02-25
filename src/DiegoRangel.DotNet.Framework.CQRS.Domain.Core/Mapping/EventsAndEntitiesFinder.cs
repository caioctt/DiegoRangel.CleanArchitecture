using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.MediatR;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Mapping
{
    public static class EventsAndEntitiesFinder
    {
        public static List<Tuple<Type, Type>> GetMappingsFrom(params Assembly[] assemblies)
        {
            var mappings = new List<Tuple<Type, Type>>();
            var types = assemblies.SelectMany(x => x.GetTypes()).ToList();

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