using System;
using System.Linq;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities
{
    /// <summary>
    /// A shortcut of <see cref="Entity{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    public abstract class IdentityRoleEntity : IdentityRoleEntity<int>
    {

    }

    /// <summary>
    /// Basic implementation of IEntity interface.
    /// An entity can inherit this class of directly implement to IEntity interface.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
    public abstract class IdentityRoleEntity<TPrimaryKey> : IdentityRole<TPrimaryKey>, IEntity<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        protected IdentityRoleEntity()
        {
            ValidationResult = new ValidationResult();
        }

        public ValidationResult ValidationResult { get; set; }

        public bool IsValid()
        {
            var type = GetType();
            var validador = ValidatorHelper.GetFrom(type);
            if (validador == null)
                throw new ArgumentException($"Validator not found for {type.Name}");

            var validationResult = validador.Validate(this);

            if (!validationResult.Errors.Any()) return ValidationResult.IsValid;
            foreach (var error in validationResult.Errors)
                ValidationResult.Errors.Add(error);

            return ValidationResult.IsValid;
        }
        public void AddValidationError(string prop, string message)
        {
            ValidationResult.Errors.Add(new ValidationFailure(prop, message));
        }
    }
}