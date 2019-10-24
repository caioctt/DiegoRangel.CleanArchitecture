using System;
using System.Collections.Generic;
using System.Linq;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Session;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.Data.EFCore.Services
{
    public class EntitiesAuditer : IEntitiesAuditer
    {
        private readonly ILoggedInUserProvider _loggedInUser;
        public EntitiesAuditer(ILoggedInUserProvider loggedInUser)
        {
            _loggedInUser = loggedInUser;
        }

        public void Analise(ChangeTracker changeTracker)
        {
            var entries = GetChangesFrom(changeTracker);

            foreach (var entry in entries)
                ApplyChanges(entry, (IAudited)entry.Entity, DateTime.Now);
        }

        private IEnumerable<EntityEntry> GetChangesFrom(ChangeTracker changeTracker)
        {
            return changeTracker.Entries()
                .Where(x => x.Entity.GetType().IsClass
                            && x.Entity.GetType().GetInterfaces().Contains(typeof(IAudited))
                            && (x.State == EntityState.Modified || x.State == EntityState.Added || x.State == EntityState.Deleted));
        }

        private void ApplyChanges(EntityEntry entityEntry, IAudited entity, DateTime data)
        {
            var user = _loggedInUser.GetLoggedInUser();

            switch (entityEntry.State)
            {
                case EntityState.Modified:
                    if (entity is IDeletionAudited deletableEntity && deletableEntity.IsDeleted && !deletableEntity.DeletionTime.HasValue)
                    {
                        deletableEntity.DeletionTime = data;
                        deletableEntity.DeleterUserId = user.Id;
                    }
                    else
                    {
                        entity.LastModificationTime = data;
                        entity.LastModifierUserId = user.Id;
                    }

                    break;
                case EntityState.Added:
                    entity.CreationTime = data;
                    entity.CreatorUserId = user.Id;
                    break;
            }
        }
    }
}