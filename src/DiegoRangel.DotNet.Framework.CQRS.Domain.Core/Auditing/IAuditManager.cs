using System;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing
{
    public interface IAuditManager
    {
        void AuditCreation<TEntityPrimaryKey, TUserPrimaryKey>(IDomainEntity entity) 
            where TEntityPrimaryKey : struct
            where TUserPrimaryKey : struct;

        void AuditCreation(IDomainEntity entity, params Type[] keyTypes);

        void AuditModification<TEntityPrimaryKey, TUserPrimaryKey>(IDomainEntity entity)
            where TEntityPrimaryKey : struct
            where TUserPrimaryKey : struct;

        void AuditModification(IDomainEntity entity, params Type[] keyTypes);

        void AuditDeletion<TEntityPrimaryKey, TUserPrimaryKey>(IDomainEntity entity)
            where TEntityPrimaryKey : struct
            where TUserPrimaryKey : struct;

        void AuditDeletion(IDomainEntity entity, params Type[] keyTypes);
    }
}