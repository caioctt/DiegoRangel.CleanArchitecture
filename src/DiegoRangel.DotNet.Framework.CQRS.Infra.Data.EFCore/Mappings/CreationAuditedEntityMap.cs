using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Session;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.Data.EFCore.Mappings
{
    public abstract class CreationAuditedEntityMap<TEntity, TPrimaryKey> : EntityMap<TEntity, TPrimaryKey>
        where TPrimaryKey : struct
        where TEntity : CreationAuditedEntity<TPrimaryKey>
    {
        public override void ConfigureEntityBuilder(EntityTypeBuilder<TEntity> builder)
        {
            ConfigureCreationAuditedEntityBuilder(builder);

            builder.Property(x => x.CreationTime).IsRequired();
        }

        public abstract void ConfigureCreationAuditedEntityBuilder(EntityTypeBuilder<TEntity> builder);
    }


    public abstract class CreationAuditedEntityMap<TEntity, TUser, TPrimaryKey> : CreationAuditedEntityMap<TEntity, TPrimaryKey>
        where TPrimaryKey : struct
        where TEntity : CreationAuditedEntity<TUser, TPrimaryKey>
        where TUser : Entity<TPrimaryKey>, IUser<TPrimaryKey>
    {
        public override void ConfigureEntityBuilder(EntityTypeBuilder<TEntity> builder)
        {
            ConfigureCreationAuditedEntityBuilder(builder);

            builder.Property(x => x.CreationTime).IsRequired();
            builder.HasOne(x => x.CreatorUser).WithMany();
        }
    }

    public abstract class CreationAuditedEntityMap<TEntity> : CreationAuditedEntityMap<TEntity, int>
        where TEntity : CreationAuditedEntity<int>
    {

    }
}