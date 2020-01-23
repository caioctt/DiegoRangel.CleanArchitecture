using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories;
using DiegoRangel.DotNet.Framework.CQRS.Infra.Data.MongoDB.Context;
using Humanizer;
using MongoDB.Driver;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.Data.MongoDB.Repositories
{
    /// <summary>
    /// The toppest level repository which all repositories should inherit from.
    /// </summary>
    public abstract class Repository<TEntity, TEntityKey> : IRepository
        where TEntity : Entity<TEntityKey>
    {
        protected readonly IMongoContext Context;
        protected readonly IMongoCollection<TEntity> DbSet;

        protected Repository(IMongoContext context)
        {
            Context = context;
            DbSet = Context.GetCollection<TEntity>(typeof(TEntity).Name.Camelize().Pluralize(false));
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
        protected Repository(IMongoContext context) : base(context)
        {
        }
    }
}