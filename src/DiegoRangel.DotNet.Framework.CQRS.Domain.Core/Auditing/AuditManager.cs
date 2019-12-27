using System;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Session;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing
{
    public class AuditManager : IAuditManager
    {
        private readonly ILoggedInUserProvider _loggedInUserProvider;
        public AuditManager(ILoggedInUserProvider loggedInUserProvider)
        {
            _loggedInUserProvider = loggedInUserProvider;
        }

        public void AuditCreation(IEntity entity)
        {
            if (!(entity is ICreationAudited creationAuditedEntity)) return;
            var user = _loggedInUserProvider.GetLoggedInUser();
            creationAuditedEntity.CreationTime = DateTime.Now;
            creationAuditedEntity.CreatorUserId = user.Id;
        }

        public void AuditModification(IEntity entity)
        {
            if (!(entity is IModificationAudited modificationAuditedEntity)) return;
            var user = _loggedInUserProvider.GetLoggedInUser();
            modificationAuditedEntity.LastModificationTime = DateTime.Now;
            modificationAuditedEntity.LastModifierUserId = user.Id;
        }

        public void AuditDeletion(IEntity entity)
        {
            if (!(entity is IDeletionAudited deletionAuditedEntity)) return;
            var user = _loggedInUserProvider.GetLoggedInUser();
            deletionAuditedEntity.DeletionTime = DateTime.Now;
            deletionAuditedEntity.DeleterUserId = user.Id;
        }
    }
}