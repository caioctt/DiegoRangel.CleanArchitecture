namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities
{
    /// <summary>
    /// Defines a shortcut interface for base Unique identifier abstraction for most used primary key type (<see cref="int"/>).
    /// </summary>
    public interface IMustHaveId : IMustHaveId<int>
    {

    }

    /// <summary>
    /// Defines an interface for base Unique identifier abstraction.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key</typeparam>
    public interface IMustHaveId<TPrimaryKey>
    {
        /// <summary>
        /// Unique identifier of type <see cref="TPrimaryKey"/>.
        /// </summary>
        TPrimaryKey Id { get; set; }
    }
}