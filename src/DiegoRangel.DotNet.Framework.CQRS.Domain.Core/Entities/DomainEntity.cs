using System;
using System.Linq;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Validators;
using FluentValidation.Results;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities
{
    public abstract class DomainEntity : IDomainEntity
    {
        protected DomainEntity()
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