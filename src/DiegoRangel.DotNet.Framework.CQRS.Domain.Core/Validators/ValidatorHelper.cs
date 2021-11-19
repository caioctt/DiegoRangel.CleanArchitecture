using System;
using System.Linq;
using System.Reflection;
using FluentValidation;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Validators
{
    public static class ValidatorHelper
    {
        public static IValidator GetFrom(Type type)
        {
            var types = Assembly.GetAssembly(type)?.GetTypes();
            var validators = types?.Where(x => x.GetInterface(nameof(IValidator)) != null);
            var validator = validators?.FirstOrDefault(x => x.BaseType != null && x.BaseType.GetGenericArguments().Contains(type));

            return validator != null
                ? (IValidator)Activator.CreateInstance(validator)
                : null;
        }
    }
}