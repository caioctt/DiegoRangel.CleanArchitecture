using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Session;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.Data.EFCore.Mappings
{
    public abstract class FullAuditedEntityMap<TEntity, TEntityKey, TUserPrimaryKey> : EntityMap<TEntity, TEntityKey>
        where TEntityKey : struct
        where TUserPrimaryKey : struct
        where TEntity : Entity<TEntityKey>, IFullAudited<TUserPrimaryKey>
    {
        public override void ConfigureEntityBuilder(EntityTypeBuilder<TEntity> builder)
        {
            ConfigureFullAuditedEntityBuilder(builder);

            builder.Property(x => x.CreationTime).IsRequired();
            builder.Property(x => x.LastModificationTime).IsRequired();
            builder.Property(x => x.DeletionTime).IsRequired();
            builder.Property(x => x.CreatorUserId).IsRequired();
            builder.Property(x => x.LastModifierUserId).IsRequired();
            builder.Property(x => x.DeleterUserId).IsRequired();
        }

        public abstract void ConfigureFullAuditedEntityBuilder(EntityTypeBuilder<TEntity> builder);
    }

    public abstract class FullAuditedEntityMap<TEntity, TEntityKey, TUser, TUserPrimaryKey> : FullAuditedEntityMap<TEntity, TEntityKey, TUserPrimaryKey>
        where TEntityKey : struct
        where TUserPrimaryKey : struct
        where TEntity : Entity<TEntityKey>, IFullAudited<TUser, TUserPrimaryKey>
        where TUser : Entity<TUserPrimaryKey>, IUser<TUserPrimaryKey>
    {
        public override void ConfigureFullAuditedEntityBuilder(EntityTypeBuilder<TEntity> builder)
        {
            ConfigureFullAuditedEntityWithUserBuilder(builder);

            builder.HasOne(x => x.CreatorUser).WithMany();
            builder.HasOne(x => x.LastModifierUser).WithMany();
            builder.HasOne(x => x.DeleterUser).WithMany();
        }

        public abstract void ConfigureFullAuditedEntityWithUserBuilder(EntityTypeBuilder<TEntity> builder);
    }

    public abstract class FullAuditedEntityMap<TEntity> : FullAuditedEntityMap<TEntity, int, int>
        where TEntity : Entity<int>, IFullAudited<int>
    {

    }
}