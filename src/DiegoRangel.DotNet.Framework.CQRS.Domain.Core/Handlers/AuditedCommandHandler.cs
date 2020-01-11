using AutoMapper;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Commands;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Interfaces;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Notifications;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories.Agregations;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Messages;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Handlers
{
    public abstract class AuditedCommandHandler<TEntity, TEntityKey, TUserKey, TDelete> :
        CreationAuditedCommandHandler<TEntity, TEntityKey, TUserKey, TDelete>
        where TEntityKey : struct
        where TUserKey : struct
        where TEntity : IAudited<TEntityKey, TUserKey>
        where TDelete : ICommandWithId<TEntityKey>
    {
        protected AuditedCommandHandler(
            DomainNotificationContext domainNotificationContext,
            CommonMessages commonMessages,
            IUnitOfWork uow,
            IAuditedRepository<TEntity, TEntityKey, TUserKey> repository) : base(domainNotificationContext, commonMessages, uow, repository)
        {
        }
    }

    public abstract class AuditedCommandHandler<TEntity, TEntityKey, TUserKey, TUpdate, TDelete> :
        CreationAuditedCommandHandler<TEntity, TEntityKey, TUserKey, TUpdate, TDelete>
        where TEntityKey : struct
        where TUserKey : struct
        where TEntity : IAudited<TEntityKey, TUserKey>
        where TUpdate : ICommandMappedWithId<TEntity, TEntityKey>
        where TDelete : ICommandWithId<TEntityKey>
    {
        protected AuditedCommandHandler(
            DomainNotificationContext domainNotificationContext,
            CommonMessages commonMessages,
            IMapper mapper,
            IUnitOfWork uow,
            IAuditedRepository<TEntity, TEntityKey, TUserKey> repository) : base(domainNotificationContext, commonMessages, mapper, uow, repository)
        {
        }
    }

    public abstract class AuditedCommandHandler<TEntity, TEntityKey, TUserKey, TRegister, TUpdate, TDelete> :
        CreationAuditedCommandHandler<TEntity, TEntityKey, TUserKey, TRegister, TUpdate, TDelete>
        where TEntityKey : struct
        where TUserKey : struct
        where TEntity : IAudited<TEntityKey, TUserKey>
        where TRegister : ICommandMapped<TEntity, TEntityKey>
        where TUpdate : ICommandMappedWithId<TEntity, TEntityKey>
        where TDelete : ICommandWithId<TEntityKey>
    {
        protected AuditedCommandHandler(
            DomainNotificationContext domainNotificationContext,
            CommonMessages commonMessages,
            IMapper mapper,
            IUnitOfWork uow,
            IAuditedRepository<TEntity, TEntityKey, TUserKey> repository) : base(domainNotificationContext, commonMessages, mapper, uow, repository)
        {
        }
    }

    public abstract class AuditedCommandHandlerBase<TEntity, TRegister, TUpdate, TDelete> :
        AuditedCommandHandler<TEntity, int, int, TRegister, TUpdate, TDelete>
        where TEntity : IAudited<int, int>
        where TRegister : ICommandMapped<TEntity, int>
        where TUpdate : ICommandMappedWithId<TEntity, int>
        where TDelete : ICommandWithId<int>
    {
        protected AuditedCommandHandlerBase(
            DomainNotificationContext domainNotificationContext,
            CommonMessages commonMessages,
            IMapper mapper,
            IUnitOfWork uow,
            IAuditedRepository<TEntity, int, int> repository) : base(domainNotificationContext, commonMessages, mapper, uow, repository)
        {
        }
    }
}