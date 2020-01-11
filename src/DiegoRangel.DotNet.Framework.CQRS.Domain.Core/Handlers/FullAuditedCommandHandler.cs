using AutoMapper;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Commands;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Interfaces;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Notifications;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories.Agregations;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Messages;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Handlers
{
    public abstract class FullAuditedCommandHandler<TEntity, TEntityKey, TUserKey, TDelete> :
        AuditedCommandHandler<TEntity, TEntityKey, TUserKey, TDelete>
        where TEntityKey : struct
        where TUserKey : struct
        where TEntity : IFullAudited<TEntityKey, TUserKey>
        where TDelete : ICommandWithId<TEntityKey>
    {
        protected FullAuditedCommandHandler(
            DomainNotificationContext domainNotificationContext,
            CommonMessages commonMessages,
            IUnitOfWork uow,
            IFullAuditedRepository<TEntity, TEntityKey, TUserKey> repository) : base(domainNotificationContext, commonMessages, uow, repository)
        {
        }
    }

    public abstract class FullAuditedCommandHandler<TEntity, TEntityKey, TUserKey, TUpdate, TDelete> :
        AuditedCommandHandler<TEntity, TEntityKey, TUserKey, TUpdate, TDelete>
        where TEntityKey : struct
        where TUserKey : struct
        where TEntity : IFullAudited<TEntityKey, TUserKey>
        where TUpdate : ICommandMappedWithId<TEntity, TEntityKey>
        where TDelete : ICommandWithId<TEntityKey>
    {
        protected FullAuditedCommandHandler(
            DomainNotificationContext domainNotificationContext,
            CommonMessages commonMessages,
            IMapper mapper,
            IUnitOfWork uow,
            IFullAuditedRepository<TEntity, TEntityKey, TUserKey> repository) : base(domainNotificationContext, commonMessages, mapper, uow, repository)
        {
        }
    }

    public abstract class FullAuditedCommandHandler<TEntity, TEntityKey, TUserKey, TRegister, TUpdate, TDelete> :
        AuditedCommandHandler<TEntity, TEntityKey, TUserKey, TRegister, TUpdate, TDelete>
        where TEntityKey : struct
        where TUserKey : struct
        where TEntity : IFullAudited<TEntityKey, TUserKey>
        where TRegister : ICommandMapped<TEntity, TEntityKey>
        where TUpdate : ICommandMappedWithId<TEntity, TEntityKey>
        where TDelete : ICommandWithId<TEntityKey>
    {
        protected FullAuditedCommandHandler(
            DomainNotificationContext domainNotificationContext,
            CommonMessages commonMessages,
            IMapper mapper,
            IUnitOfWork uow,
            IFullAuditedRepository<TEntity, TEntityKey, TUserKey> repository) : base(domainNotificationContext, commonMessages, mapper, uow, repository)
        {
        }
    }

    public abstract class FullAuditedCommandHandlerBase<TEntity, TRegister, TUpdate, TDelete> :
        FullAuditedCommandHandler<TEntity, int, int, TRegister, TUpdate, TDelete>
        where TEntity : IFullAudited<int, int>
        where TRegister : ICommandMapped<TEntity, int>
        where TUpdate : ICommandMappedWithId<TEntity, int>
        where TDelete : ICommandWithId<int>
    {
        protected FullAuditedCommandHandlerBase(
            DomainNotificationContext domainNotificationContext,
            CommonMessages commonMessages,
            IMapper mapper,
            IUnitOfWork uow,
            IFullAuditedRepository<TEntity, int, int> repository) : base(domainNotificationContext, commonMessages, mapper, uow, repository)
        {
        }
    }
}