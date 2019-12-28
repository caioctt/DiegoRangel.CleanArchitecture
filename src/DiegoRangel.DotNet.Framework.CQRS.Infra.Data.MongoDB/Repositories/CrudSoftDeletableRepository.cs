using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories;
using DiegoRangel.DotNet.Framework.CQRS.Infra.Data.MongoDB.Context;
using MongoDB.Driver;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.Data.MongoDB.Repositories
{
    public abstract class CrudSoftDeletableRepository<TEntity, TEntityKey, TUserPrimaryKey> : 
        CrudRepository<TEntity, TEntityKey, TUserPrimaryKey>, 
        ICrudSoftDeletableRepository<TEntity, TEntityKey>
        where TEntityKey : struct
        where TUserPrimaryKey : struct
        where TEntity : Entity<TEntityKey>, ISoftDelete
    {
        private readonly IAuditManager _auditManager;

        protected CrudSoftDeletableRepository(IMongoContext context, IAuditManager auditManager) : base(context, auditManager)
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
            _auditManager.AuditDeletion(entity as IEntity);

            Context.AddCommand(() => DbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq(x => x.Id, entity.Id), entity));
            return Task.CompletedTask;
        }

        public virtual async Task MoveToTrashAsync(IList<TEntity> entities)
        {
            foreach (var entity in entities)
                await MoveToTrashAsync(entity);
        }

        protected override FilterDefinition<TEntity> BuildFindFilters(Expression<Func<TEntity, bool>> predicate = null)
        {
            var baseFilter = base.BuildFindFilters(predicate);
            var isDeletedFlter = Builders<TEntity>.Filter.Eq(x => x.IsDeleted, false);

            return Builders<TEntity>.Filter.And(baseFilter, isDeletedFlter);
        }
    }

    public abstract class CrudSoftDeletableRepository<TEntity> : CrudSoftDeletableRepository<TEntity, int, int>
        where TEntity : Entity<int>, ISoftDelete
    {
        protected CrudSoftDeletableRepository(IMongoContext context, IAuditManager auditManager) : base(context, auditManager)
        {
        }
    }
}