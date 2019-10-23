namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities
{
    public interface IPassivable
    {
        /// <summary>
        /// True: This entity is active.
        /// False: This entity is not active.
        /// </summary>
        bool IsActive { get; set; }
    }
}