using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Session;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.Data.EFCore.Mappings
{
    public abstract class FullAuditedEntityMap<TEntity, TEntityKey, TUserKey> : 
        EntityMap<TEntity, TEntityKey>
        where TEntity : Entity<TEntityKey>, IFullAudited<TEntityKey, TUserKey>
        where TUserKey : struct
    {
        public override void ConfigureEntityBuilder(EntityTypeBuilder<TEntity> builder)
        {
            ConfigureFullAuditedEntityBuilder(builder);

            builder.Property(x => x.CreationTime).IsRequired();
            builder.Property(x => x.LastModificationTime).IsRequired(false);
            builder.Property(x => x.DeletionTime).IsRequired(false);
            builder.Property(x => x.CreatorUserId).IsRequired();
            builder.Property(x => x.LastModifierUserId).IsRequired(false);
            builder.Property(x => x.DeleterUserId).IsRequired(false);
        }

        public abstract void ConfigureFullAuditedEntityBuilder(EntityTypeBuilder<TEntity> builder);
    }

    public abstract class FullAuditedEntityMap<TEntity, TEntityKey, TUserKey, TUser> : 
        FullAuditedEntityMap<TEntity, TEntityKey, TUserKey>
        where TEntity : Entity<TEntityKey>, IFullAudited<TEntityKey, TUserKey, TUser>
        where TUser : Entity<TUserKey>, IUser<TUserKey>
        where TUserKey : struct
    {
        public override void ConfigureFullAuditedEntityBuilder(EntityTypeBuilder<TEntity> builder)
        {
            ConfigureFullAuditedEntityWithUserBuilder(builder);

            builder.HasOne(x => x.CreatorUser).WithMany().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.LastModifierUser).WithMany().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.DeleterUser).WithMany().OnDelete(DeleteBehavior.Restrict);
        }

        public abstract void ConfigureFullAuditedEntityWithUserBuilder(EntityTypeBuilder<TEntity> builder);
    }

    public abstract class FullAuditedEntityMap<TEntity> : 
        FullAuditedEntityMap<TEntity, int, int>
        where TEntity : Entity<int>, IFullAudited<int, int>
    {

    }
}