namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities
{
    public interface ISoftDelete
    {
        /// <summary>
        /// Indicates if the entity is on the "Trash".
        /// </summary>
        bool IsDeleted { get; }

        /// <summary>
        /// Used to move an Entity to the "Trash".
        /// </summary>
        void MoveToTrash();
    }
}