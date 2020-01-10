using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories.Agregations;
using DiegoRangel.DotNet.Framework.CQRS.Infra.Data.MongoDB.Context;
using MongoDB.Driver;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.Data.MongoDB.Repositories
{
    public abstract class CreationAuditedRepository<TEntity, TEntityKey, TUserKey> :
        CrudRepository<TEntity, TEntityKey>,
        ICreationAuditedRepository<TEntity, TEntityKey, TUserKey>
        where TEntityKey : struct
        where TUserKey : struct
        where TEntity : CreationAuditedEntity<TEntityKey, TUserKey>
    {
        private readonly IAuditManager _auditManager;
        
        protected CreationAuditedRepository(IMongoContext context, IAuditManager auditManager) : base(context)
        {
            _auditManager = auditManager;
        }

        public override Task AddAsync(TEntity entity)
        {
            _auditManager.AuditCreation<TEntityKey>(entity);
            return base.AddAsync(entity);
        }

        protected override SortDefinition<TEntity> BuildDefaultSortDefinition()
        {
            return Builders<TEntity>.Sort.Descending(x => x.CreationTime);
        }
    }

    public abstract class CreationAuditedRepository<TEntity> : CreationAuditedRepository<TEntity, int, int>
        where TEntity : CreationAuditedEntity<int, int>
    {
        protected CreationAuditedRepository(IMongoContext context, IAuditManager auditManager) : base(context, auditManager)
        {
        }
    }
}