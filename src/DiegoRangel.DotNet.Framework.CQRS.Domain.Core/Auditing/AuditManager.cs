using System;
using System.Linq;
using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Session;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing
{
    public class AuditManager<TUserKey> : IAuditManager
        where TUserKey : struct
    {
        private readonly ILoggedInUserIdProvider<TUserKey> _loggedInUserIdProvider;
        public AuditManager(ILoggedInUserIdProvider<TUserKey> loggedInUserIdProvider)
        {
            _loggedInUserIdProvider = loggedInUserIdProvider;
        }

        public async Task AuditCreation<TEntityPrimaryKey>(IDomainEntity entity)
        {
            await TrySetCreationAuditsFor<TEntityPrimaryKey>(entity);
        }
        public Task AuditCreation(IDomainEntity entity, params Type[] keyTypes)
        {
            var method = GetType().GetMethods().FirstOrDefault(x => x.Name.Equals("AuditCreation") && x.ContainsGenericParameters);
            method?.MakeGenericMethod(keyTypes).Invoke(this, new object[] { entity });
            return Task.CompletedTask;
        }

        public async Task AuditModification<TEntityPrimaryKey>(IDomainEntity entity)
        {
            await TrySetModificationAuditsFor<TEntityPrimaryKey>(entity);
        }
        public Task AuditModification(IDomainEntity entity, params Type[] keyTypes)
        {
            var method = GetType().GetMethods().FirstOrDefault(x => x.Name.Equals("AuditModification") && x.ContainsGenericParameters);
            method?.MakeGenericMethod(keyTypes).Invoke(this, new object[] { entity });
            return Task.CompletedTask;
        }

        public async Task AuditDeletion<TEntityPrimaryKey>(IDomainEntity entity)
        {
            if (!(entity is IDeletionAudited<TEntityPrimaryKey, TUserKey> deletionAuditedEntity)) return;
            if (!deletionAuditedEntity.IsDeleted || deletionAuditedEntity.DeletionTime.HasValue) return;

            var userId = await _loggedInUserIdProvider.GetUserLoggedInIdAsync();
            deletionAuditedEntity.DeletionTime = DateTime.Now;
            deletionAuditedEntity.DeleterUserId = userId;
        }
        public Task AuditDeletion(IDomainEntity entity, params Type[] keyTypes)
        {
            var method = GetType().GetMethods().FirstOrDefault(x => x.Name.Equals("AuditDeletion") && x.ContainsGenericParameters);
            method?.MakeGenericMethod(keyTypes).Invoke(this, new object[] { entity });
            return Task.CompletedTask;
        }

        private async Task TrySetCreationAuditsFor<TEntityPrimaryKey>(IDomainEntity entity)
        {
            if (!(entity is ICreationAudited<TEntityPrimaryKey, TUserKey> creationAuditedEntity)) return;
            var userId = await _loggedInUserIdProvider.GetUserLoggedInIdAsync();
            creationAuditedEntity.CreationTime = DateTime.Now;
            creationAuditedEntity.CreatorUserId = userId;
        }
        private async Task TrySetModificationAuditsFor<TEntityPrimaryKey>(IDomainEntity entity)
        {
            if (!(entity is IModificationAudited<TEntityPrimaryKey, TUserKey> modificationAuditedEntity)) return;
            var userId = await _loggedInUserIdProvider.GetUserLoggedInIdAsync();
            modificationAuditedEntity.LastModificationTime = DateTime.Now;
            modificationAuditedEntity.LastModifierUserId = userId;
        }
    }
}