using System;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Session;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing
{
    public class AuditManager<TUserPrimaryKey> : IAuditManager
    {
        private readonly ILoggedInUserProvider<IUser<TUserPrimaryKey>, TUserPrimaryKey> _loggedInUserProvider;
        public AuditManager(ILoggedInUserProvider<IUser<TUserPrimaryKey>, TUserPrimaryKey> loggedInUserProvider)
        {
            _loggedInUserProvider = loggedInUserProvider;
        }

        public void AuditCreation<TEntityPrimaryKey>(IDomainEntity entity) 
        {
            if (!(entity is ICreationAudited<TEntityPrimaryKey, TUserPrimaryKey> creationAuditedEntity)) return;
            var user = _loggedInUserProvider.GetLoggedInUser();
            creationAuditedEntity.CreationTime = DateTime.Now;
            creationAuditedEntity.CreatorUserId = user.Id;
        }
        public void AuditCreation(IDomainEntity entity, params Type[] keyTypes)
        {
            GetType().GetMethod("AuditCreation")?.MakeGenericMethod(keyTypes).Invoke(null, new object[] {entity});
        }

        public void AuditModification<TEntityPrimaryKey>(IDomainEntity entity)
        {
            if (!(entity is IModificationAudited<TEntityPrimaryKey, TUserPrimaryKey> modificationAuditedEntity)) return;
            var user = _loggedInUserProvider.GetLoggedInUser();
            modificationAuditedEntity.LastModificationTime = DateTime.Now;
            modificationAuditedEntity.LastModifierUserId = user.Id;
        }
        public void AuditModification(IDomainEntity entity, params Type[] keyTypes)
        {
            GetType().GetMethod("AuditModification")?.MakeGenericMethod(keyTypes).Invoke(null, new object[] { entity });
        }

        public void AuditDeletion<TEntityPrimaryKey>(IDomainEntity entity)
        {
            if (!(entity is IDeletionAudited<TEntityPrimaryKey, TUserPrimaryKey> deletionAuditedEntity)) return;
            if (!deletionAuditedEntity.IsDeleted || deletionAuditedEntity.DeletionTime.HasValue) return;

            var user = _loggedInUserProvider.GetLoggedInUser();
            deletionAuditedEntity.DeletionTime = DateTime.Now;
            deletionAuditedEntity.DeleterUserId = user.Id;
        }
        public void AuditDeletion(IDomainEntity entity, params Type[] keyTypes)
        {
            GetType().GetMethod("AuditDeletion")?.MakeGenericMethod(keyTypes).Invoke(null, new object[] { entity });
        }
    }
}