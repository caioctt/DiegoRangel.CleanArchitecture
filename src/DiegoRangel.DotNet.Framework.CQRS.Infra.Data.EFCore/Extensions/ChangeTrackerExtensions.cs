using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.Data.EFCore.Extensions
{
    public static class ChangeTrackerExtensions
    {
        public static IEnumerable<EntityEntry> Entries<T>(this ChangeTracker changeTracker, Func<EntityEntry<T>, bool> condicao) where T : class
        {
            return changeTracker.Entries<T>().Where(condicao);
        }
    }
}