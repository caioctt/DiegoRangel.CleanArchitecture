using System;
using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing
{
    public interface IAuditManager
    {
        Task AuditCreation<TEntityPrimaryKey>(IDomainEntity entity);

        Task AuditCreation(IDomainEntity entity, params Type[] keyTypes);

        Task AuditModification<TEntityPrimaryKey>(IDomainEntity entity);

        Task AuditModification(IDomainEntity entity, params Type[] keyTypes);

        Task AuditDeletion<TEntityPrimaryKey>(IDomainEntity entity);

        Task AuditDeletion(IDomainEntity entity, params Type[] keyTypes);
    }
}