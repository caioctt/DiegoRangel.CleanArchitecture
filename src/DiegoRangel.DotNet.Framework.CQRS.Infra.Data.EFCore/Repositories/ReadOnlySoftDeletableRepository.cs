using System.Linq;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.Data.EFCore.Repositories
{
    public abstract class ReadOnlySoftDeletableRepository<TEntity, TPrimaryKey> : ReadOnlyRepository<TEntity, TPrimaryKey>, IReadOnlySoftDeletableRepository<TEntity, TPrimaryKey>
        where TPrimaryKey : struct
        where TEntity : Entity<TPrimaryKey>, ISoftDelete
    {
        protected override IQueryable<TEntity> Query => DbSet.Where(x => !x.IsDeleted);

        protected ReadOnlySoftDeletableRepository(DbContext context) : base(context)
        {
        }
    }

    public abstract class ReadOnlySoftDeletableRepository<TEntity> : ReadOnlySoftDeletableRepository<TEntity, int>
        where TEntity : Entity<int>, ISoftDelete
    {
        protected ReadOnlySoftDeletableRepository(DbContext context) : base(context)
        {
        }
    }
}