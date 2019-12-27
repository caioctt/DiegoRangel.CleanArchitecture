using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing
{
    public interface IAuditManager
    {
        void AuditCreation(IEntity entity);
        void AuditModification(IEntity entity);
        void AuditDeletion(IEntity entity);
    }
}