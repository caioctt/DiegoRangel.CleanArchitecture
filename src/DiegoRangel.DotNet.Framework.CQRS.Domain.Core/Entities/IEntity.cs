using FluentValidation.Results;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities
{
    /// <summary>
    /// A shortcut of <see cref="IEntity{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    public interface IEntity : IEntity<int>
    {

    }

    /// <summary>
    /// Defines interface for base entity type. All entities in the system must implement this interface.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
    public interface IEntity<TPrimaryKey> : IMustHaveId<TPrimaryKey>, IDomainEntity
    {
        
    }
}