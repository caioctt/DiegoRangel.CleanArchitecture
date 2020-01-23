using System.Collections.Generic;
using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories.Agregations;
using DiegoRangel.DotNet.Framework.CQRS.Infra.Data.MongoDB.Context;
using MongoDB.Driver;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.Data.MongoDB.Repositories
{
    public abstract class FullAuditedRepository<TEntity, TEntityKey, TUserKey> :
        AuditedRepository<TEntity, TEntityKey, TUserKey>,
        IFullAuditedRepository<TEntity, TEntityKey, TUserKey>
        where TEntity : FullAuditedEntity<TEntityKey, TUserKey>
    {
        private readonly IAuditManager _auditManager;

        protected FullAuditedRepository(IMongoContext context, IAuditManager auditManager) : base(context, auditManager)
        {
            _auditManager = auditManager;
        }

        public virtual async Task MoveToTrashAsync(TEntityKey id)
        {
            var entity = await FindByIdAsync(id);
            await MoveToTrashAsync(entity);
        }

        public virtual Task MoveToTrashAsync(TEntity entity)
        {
            entity.MoveToTrash();
            _auditManager.AuditDeletion(entity);

            Context.AddCommand(() => DbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq(x => x.Id, entity.Id), entity));
            return Task.CompletedTask;
        }

        public virtual async Task MoveToTrashAsync(IList<TEntity> entities)
        {
            foreach (var entity in entities)
                await MoveToTrashAsync(entity);
        }

        protected override FilterDefinition<TEntity> BuildDefaultFilterDefinition()
        {
            return Builders<TEntity>.Filter.Eq(x => x.IsDeleted, false);
        }
    }

    public abstract class FullAuditedRepository<TEntity> : FullAuditedRepository<TEntity, int, int>
        where TEntity : FullAuditedEntity<int, int>
    {
        protected FullAuditedRepository(IMongoContext context, IAuditManager auditManager) : base(context, auditManager)
        {

        }
    }
}