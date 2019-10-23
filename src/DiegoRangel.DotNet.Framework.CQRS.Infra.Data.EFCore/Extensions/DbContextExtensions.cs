using System.Linq;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.Data.EFCore.Extensions
{
    public static class DbContextExtensions
    {
        public static IQueryable<TEntity> QueryOf<TEntity>(this DbContext context)
            where TEntity : class, IEntity<int>
        {
            return context.QueryOf<TEntity, int>();
        }

        public static IQueryable<TEntity> QueryOf<TEntity, TKey>(this DbContext context)
            where TEntity : class, IEntity<TKey>
        {
            return context
                .Set<TEntity>()
                .Where(x => !(x is ISoftDelete) 
                            || !((ISoftDelete) x).IsDeleted);
        }
    }
}