namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities
{
    public interface ISoftDelete
    {
        /// <summary>
        /// Used to move an Entity to the "Trash".
        /// </summary>
        bool IsDeleted { get; set; }
    }
}