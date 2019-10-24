using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.Data.EFCore.Repositories
{
    /// <summary>
    /// The toppest level repository which all repositories should inherit from.
    /// </summary>
    public abstract class Repository<TEntity, TEntityKey> : IRepository
        where TEntityKey : struct
        where TEntity : Entity<TEntityKey>
    {
        protected readonly DbContext Context;
        protected readonly DbSet<TEntity> DbSet;
        
        protected Repository(DbContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }

    /// <summary>
    /// A shortcut repository for <see cref="Repository{TEntity, TEntityKey}"/> for most used primary key type (<see cref="int"/>)..
    /// </summary>
    public abstract class Repository<TEntity> : Repository<TEntity, int>
        where TEntity : Entity<int>
    {
        protected Repository(DbContext context) : base(context)
        {
        }
    }
}