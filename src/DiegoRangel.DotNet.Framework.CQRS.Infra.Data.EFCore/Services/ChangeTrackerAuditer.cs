using System.Linq;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.Data.EFCore.Services
{
    public class ChangeTrackerAuditer : IChangeTrackerAuditer
    {
        private readonly IAuditManager _auditManager;
        public ChangeTrackerAuditer(IAuditManager auditManager)
        {
            _auditManager = auditManager;
        }

        public void Audit(ChangeTracker changeTracker)
        {
            var entries = changeTracker.Entries()
                .Where(x => x.Entity.GetType().IsClass
                            && !x.Entity.GetType().IsAbstract
                            && x.Entity.GetType().GetInterfaces().Contains(typeof(IEntity))
                            && (x.State == EntityState.Added || x.State == EntityState.Modified))
                .ToList();

            if (entries.Count == 0) return;

            var creationAudits = entries.Where(x => x.State == EntityState.Added 
                                                    && x.Entity is ICreationAudited);

            foreach (var entry in creationAudits)
                _auditManager.AuditCreation(entry.Entity as IEntity);

            var modificationAudits = entries.Where(x => x.State == EntityState.Modified 
                                                        && x.Entity is IModificationAudited);

            foreach (var entry in modificationAudits)
                _auditManager.AuditModification(entry.Entity as IEntity);

            var deletionAudits = entries.Where(x => x.State == EntityState.Modified 
                                                    && x.Entity is IDeletionAudited deletableEntity 
                                                    && deletableEntity.IsDeleted 
                                                    && !deletableEntity.DeletionTime.HasValue);

            foreach (var entry in deletionAudits)
                _auditManager.AuditDeletion(entry.Entity as IEntity);
        }
    }
}