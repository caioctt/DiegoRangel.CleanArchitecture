using System;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task Audit(ChangeTracker changeTracker)
        {
            var entries = changeTracker.Entries()
                .Where(x => x.Entity.GetType().IsClass
                            && !x.Entity.GetType().IsAbstract
                            && x.Entity.GetType().GetInterfaces().Contains(typeof(IDomainEntity))
                            && (x.State == EntityState.Added || x.State == EntityState.Modified))
                .ToList();

            if (entries.Count == 0) return;

            var creationAuditedInterface = typeof(ICreationAudited<,>);
            var creationAudits = entries.Where(x => IsElegibleToAuditByInterface(x, EntityState.Added, creationAuditedInterface));

            foreach (var entry in creationAudits)
                await _auditManager.AuditCreation(entry.Entity as IDomainEntity, GetAuditableInterfaceArguments(entry, creationAuditedInterface));

            var modificationAuditedInterface = typeof(IModificationAudited<,>);
            var modificationAudits = entries.Where(x => IsElegibleToAuditByInterface(x, EntityState.Modified, modificationAuditedInterface));

            foreach (var entry in modificationAudits)
                await _auditManager.AuditModification(entry.Entity as IDomainEntity, GetAuditableInterfaceArguments(entry, modificationAuditedInterface));

            var deletionAuditedInterface = typeof(IDeletionAudited<,>);
            var deletionAudits = entries.Where(x => 
                IsElegibleToAuditByInterface(x, EntityState.Modified, deletionAuditedInterface)
                && (bool?)x.Entity.GetType().GetProperty("IsDeleted")?.GetValue(x.Entity) == true);

            foreach (var entry in deletionAudits)
                await _auditManager.AuditDeletion(entry.Entity as IDomainEntity, GetAuditableInterfaceArguments(entry, deletionAuditedInterface));
        }

        private static bool IsElegibleToAuditByInterface(EntityEntry entry, EntityState entityState, Type auditableInterface)
        {
            return entry.State == entityState 
                   && entry.Entity
                           .GetType()
                           .GetInterfaces()
                           .Any(i => i.IsGenericType
                                     && i.GetGenericTypeDefinition() == auditableInterface);
        }
        private static Type GetAuditableInterfaceArguments(EntityEntry entry, Type auditableInterface)
        {
            return entry.Entity
                       .GetType()
                       .GetInterfaces()
                       .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == auditableInterface)
                       ?.GetGenericArguments().FirstOrDefault();
        }
    }
}