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

        public void AuditCreation<TEntityPrimaryKey, TUserPrimaryKey>(IDomainEntity entity) 
            where TEntityPrimaryKey : struct
            where TUserPrimaryKey : struct
        {
            if (!(entity is ICreationAudited<TEntityPrimaryKey, TUserPrimaryKey> creationAuditedEntity)) return;
            var user = _loggedInUserProvider.GetLoggedInUser<TUserPrimaryKey>();
            creationAuditedEntity.CreationTime = DateTime.Now;
            creationAuditedEntity.CreatorUserId = user.Id;
        }
        public void AuditCreation(IDomainEntity entity, params Type[] keyTypes)
        {
            GetType().GetMethod("AuditCreation")?.MakeGenericMethod(keyTypes).Invoke(null, new object[] {entity});
        }

        public void AuditModification<TEntityPrimaryKey, TUserPrimaryKey>(IDomainEntity entity)
            where TEntityPrimaryKey : struct
            where TUserPrimaryKey : struct
        {
            if (!(entity is IModificationAudited<TEntityPrimaryKey, TUserPrimaryKey> modificationAuditedEntity)) return;
            var user = _loggedInUserProvider.GetLoggedInUser<TUserPrimaryKey>();
            modificationAuditedEntity.LastModificationTime = DateTime.Now;
            modificationAuditedEntity.LastModifierUserId = user.Id;
        }
        public void AuditModification(IDomainEntity entity, params Type[] keyTypes)
        {
            GetType().GetMethod("AuditModification")?.MakeGenericMethod(keyTypes).Invoke(null, new object[] { entity });
        }

        public void AuditDeletion<TEntityPrimaryKey, TUserPrimaryKey>(IDomainEntity entity)
            where TEntityPrimaryKey : struct
            where TUserPrimaryKey : struct
        {
            if (!(entity is IDeletionAudited<TEntityPrimaryKey, TUserPrimaryKey> deletionAuditedEntity)) return;
            if (!deletionAuditedEntity.IsDeleted || deletionAuditedEntity.DeletionTime.HasValue) return;

            var user = _loggedInUserProvider.GetLoggedInUser<TUserPrimaryKey>();
            deletionAuditedEntity.DeletionTime = DateTime.Now;
            deletionAuditedEntity.DeleterUserId = user.Id;
        }
        public void AuditDeletion(IDomainEntity entity, params Type[] keyTypes)
        {
            GetType().GetMethod("AuditDeletion")?.MakeGenericMethod(keyTypes).Invoke(null, new object[] { entity });
        }
    }
}