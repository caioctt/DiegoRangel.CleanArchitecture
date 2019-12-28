using FluentValidation.Results;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities
{
    public interface IDomainEntity
    {
        ValidationResult ValidationResult { get; set; }

        bool IsValid();
        void AddValidationError(string prop, string message);
    }
}