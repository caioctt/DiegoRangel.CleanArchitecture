using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories.Agregations;
using DiegoRangel.DotNet.Framework.CQRS.Infra.Data.MongoDB.Context;
using MongoDB.Driver;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.Data.MongoDB.Repositories
{
    public abstract class AuditedRepository<TEntity, TEntityKey, TUserKey> :
        CreationAuditedRepository<TEntity, TEntityKey, TUserKey>,
        IAuditedRepository<TEntity, TEntityKey, TUserKey>
        where TEntity : AuditedEntity<TEntityKey, TUserKey>, IAudited<TEntityKey, TUserKey>
    {
        private readonly IAuditManager _auditManager;

        protected AuditedRepository(IMongoContext context, IAuditManager auditManager) : base(context, auditManager)
        {
            _auditManager = auditManager;
        }

        public override Task UpdateAsync(TEntity entity)
        {
            _auditManager.AuditModification<TEntityKey>(entity);
            return base.UpdateAsync(entity);
        }

        protected override SortDefinition<TEntity> BuildDefaultSortDefinition()
        {
            return Builders<TEntity>.Sort.Combine(
                Builders<TEntity>.Sort.Descending(x => x.CreationTime),
                Builders<TEntity>.Sort.Descending(x => x.LastModificationTime));
        }
    }

    public abstract class AuditedRepository<TEntity> : AuditedRepository<TEntity, int, int>
        where TEntity : AuditedEntity<int, int>, IAudited<int, int>
    {
        protected AuditedRepository(IMongoContext context, IAuditManager auditManager) : base(context, auditManager)
        {
        }
    }
}