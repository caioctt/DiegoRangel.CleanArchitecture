using System;
using System.Linq;
using System.Reflection;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Extensions
{
    public static class CleanCodeServiceCollectionExtensions
    {
        public static void AddRepositoryRejectionOnControllers(this IServiceCollection services, params Assembly[] assemblies)
        {
            var types = assemblies.SelectMany(x => x.GetTypes()).ToList();
            var invalidControllers = types
                .Where(x =>
                    x.IsClass
                    && !x.IsAbstract
                    && x.IsSubclassOf(typeof(Controller))
                    && x.GetConstructors()
                        .Any(c => c.IsConstructor
                                  && c.ReflectedType is TypeInfo typeInfo
                                  && typeInfo.DeclaredFields.Any(field =>
                                      field.FieldType
                                          .GetInterfaces()
                                          .Contains(typeof(IRepository)))))
                .ToList();

            if (invalidControllers.Any())
                throw new Exception("Injections of Repositories into Controllers are not allowed. Please, use MediatR!");
        }
    }
}