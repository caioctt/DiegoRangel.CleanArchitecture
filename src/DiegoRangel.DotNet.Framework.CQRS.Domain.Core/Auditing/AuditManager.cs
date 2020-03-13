using System;
using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Session;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing
{
    public class AuditManager<TUserPrimaryKey> : IAuditManager
    {
        private readonly ILoggedInUserIdProvider<TUserPrimaryKey> _loggedInUserIdProvider;
        public AuditManager(ILoggedInUserIdProvider<TUserPrimaryKey> loggedInUserIdProvider)
        {
            _loggedInUserIdProvider = loggedInUserIdProvider;
        }

        public async Task AuditCreation<TEntityPrimaryKey>(IDomainEntity entity) 
        {
            if (!(entity is ICreationAudited<TEntityPrimaryKey, TUserPrimaryKey> creationAuditedEntity)) return;
            var userId = await _loggedInUserIdProvider.GetUserLoggedInIdAsync();
            creationAuditedEntity.CreationTime = DateTime.Now;
            creationAuditedEntity.CreatorUserId = userId;
        }
        public Task AuditCreation(IDomainEntity entity, params Type[] keyTypes)
        {
            GetType().GetMethod("AuditCreation")?.MakeGenericMethod(keyTypes).Invoke(null, new object[] {entity});
            return Task.CompletedTask;
        }

        public async Task AuditModification<TEntityPrimaryKey>(IDomainEntity entity)
        {
            if (!(entity is IModificationAudited<TEntityPrimaryKey, TUserPrimaryKey> modificationAuditedEntity)) return;
            var userId = await _loggedInUserIdProvider.GetUserLoggedInIdAsync();
            modificationAuditedEntity.LastModificationTime = DateTime.Now;
            modificationAuditedEntity.LastModifierUserId = userId;
        }
        public Task AuditModification(IDomainEntity entity, params Type[] keyTypes)
        {
            GetType().GetMethod("AuditModification")?.MakeGenericMethod(keyTypes).Invoke(null, new object[] { entity });
            return Task.CompletedTask;
        }

        public async Task AuditDeletion<TEntityPrimaryKey>(IDomainEntity entity)
        {
            if (!(entity is IDeletionAudited<TEntityPrimaryKey, TUserPrimaryKey> deletionAuditedEntity)) return;
            if (!deletionAuditedEntity.IsDeleted || deletionAuditedEntity.DeletionTime.HasValue) return;

            var userId = await _loggedInUserIdProvider.GetUserLoggedInIdAsync();
            deletionAuditedEntity.DeletionTime = DateTime.Now;
            deletionAuditedEntity.DeleterUserId = userId;
        }
        public Task AuditDeletion(IDomainEntity entity, params Type[] keyTypes)
        {
            GetType().GetMethod("AuditDeletion")?.MakeGenericMethod(keyTypes).Invoke(null, new object[] { entity });
            return Task.CompletedTask;
        }
    }
}