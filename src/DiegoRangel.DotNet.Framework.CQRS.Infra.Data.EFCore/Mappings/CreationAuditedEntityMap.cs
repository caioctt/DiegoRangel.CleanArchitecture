using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Session;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.Data.EFCore.Mappings
{
    public abstract class CreationAuditedEntityMap<TEntity, TEntityKey, TUserPrimaryKey> : EntityMap<TEntity, TEntityKey>
        where TEntity : Entity<TEntityKey>, ICreationAudited<TEntityKey, TUserPrimaryKey>
    {
        public override void ConfigureEntityBuilder(EntityTypeBuilder<TEntity> builder)
        {
            ConfigureCreationAuditedEntityBuilder(builder);

            builder.Property(x => x.CreationTime).IsRequired();
            builder.Property(x => x.CreatorUserId).IsRequired();
        }

        public abstract void ConfigureCreationAuditedEntityBuilder(EntityTypeBuilder<TEntity> builder);
    }
    
    public abstract class CreationAuditedEntityMap<TEntity, TEntityKey, TUserPrimaryKey, TUser> : CreationAuditedEntityMap<TEntity, TEntityKey, TUserPrimaryKey>
        where TEntity : Entity<TEntityKey>, ICreationAudited<TEntityKey, TUserPrimaryKey, TUser>
        where TUser : Entity<TUserPrimaryKey>, IUser<TUserPrimaryKey>
    {
        public override void ConfigureCreationAuditedEntityBuilder(EntityTypeBuilder<TEntity> builder)
        {
            ConfigureCreationAuditedEntityWithUserBuilder(builder);

            builder.HasOne(x => x.CreatorUser).WithMany();
        }

        public abstract void ConfigureCreationAuditedEntityWithUserBuilder(EntityTypeBuilder<TEntity> builder);
    }

    public abstract class CreationAuditedEntityMap<TEntity> : CreationAuditedEntityMap<TEntity, int, int>
        where TEntity : Entity<int>, ICreationAudited<int, int>
    {

    }
}