using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Session;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.Data.EFCore.Mappings
{
    public abstract class AuditedEntityMap<TEntity, TEntityKey, TUserPrimaryKey> : EntityMap<TEntity, TEntityKey>
        where TEntityKey : struct
        where TUserPrimaryKey : struct
        where TEntity : Entity<TEntityKey>, IAudited<TEntityKey, TUserPrimaryKey>
    {
        public override void ConfigureEntityBuilder(EntityTypeBuilder<TEntity> builder)
        {
            ConfigureAuditedEntityBuilder(builder);

            builder.Property(x => x.CreationTime).IsRequired();
            builder.Property(x => x.LastModificationTime).IsRequired();
            builder.Property(x => x.CreatorUserId).IsRequired();
            builder.Property(x => x.LastModifierUserId).IsRequired();
        }

        public abstract void ConfigureAuditedEntityBuilder(EntityTypeBuilder<TEntity> builder);
    }
    
    public abstract class AuditedEntityMap<TEntity, TEntityKey, TUserPrimaryKey, TUser> : AuditedEntityMap<TEntity, TEntityKey, TUserPrimaryKey>
        where TEntityKey : struct
        where TUserPrimaryKey : struct
        where TEntity : Entity<TEntityKey>, IAudited<TEntityKey, TUserPrimaryKey, TUser>
        where TUser : Entity<TUserPrimaryKey>, IUser<TUserPrimaryKey>
    {
        public override void ConfigureAuditedEntityBuilder(EntityTypeBuilder<TEntity> builder)
        {
            ConfigureAuditedEntityWithUserBuilder(builder);

            builder.HasOne(x => x.CreatorUser).WithMany();
            builder.HasOne(x => x.LastModifierUser).WithMany();
        }

        public abstract void ConfigureAuditedEntityWithUserBuilder(EntityTypeBuilder<TEntity> builder);
    }

    public abstract class AuditedEntityMap<TEntity> : AuditedEntityMap<TEntity, int, int>
        where TEntity : Entity<int>, IAudited<int, int>
    {

    }
}