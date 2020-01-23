using AutoMapper;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Commands;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Interfaces;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Notifications;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories.Agregations;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Messages;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Handlers
{
    public abstract class CreationAuditedCommandHandler<TEntity, TEntityKey, TUserKey, TDelete> :
        CrudCommandHandler<TEntity, TEntityKey, TDelete>
        where TEntity : ICreationAudited<TEntityKey, TUserKey>
        where TDelete : ICommandWithId<TEntityKey>
    {
        protected CreationAuditedCommandHandler(
            DomainNotificationContext domainNotificationContext, 
            CommonMessages commonMessages, 
            IUnitOfWork uow,
            ICreationAuditedRepository<TEntity, TEntityKey, TUserKey> repository) : base(domainNotificationContext, commonMessages, uow, repository)
        {
        }
    }

    public abstract class CreationAuditedCommandHandler<TEntity, TEntityKey, TUserKey, TUpdate, TDelete> :
        CrudCommandHandler<TEntity, TEntityKey, TUpdate, TDelete>
        where TEntity : ICreationAudited<TEntityKey, TUserKey>
        where TUpdate : ICommandMappedWithId<TEntity, TEntityKey>
        where TDelete : ICommandWithId<TEntityKey>
    {
        protected CreationAuditedCommandHandler(
            DomainNotificationContext domainNotificationContext,
            CommonMessages commonMessages,
            IMapper mapper,
            IUnitOfWork uow,
            ICreationAuditedRepository<TEntity, TEntityKey, TUserKey> repository) : base(domainNotificationContext, commonMessages, mapper, uow, repository)
        {
        }
    }

    public abstract class CreationAuditedCommandHandler<TEntity, TEntityKey, TUserKey, TRegister, TUpdate, TDelete> :
        CrudCommandHandler<TEntity, TEntityKey, TRegister, TUpdate, TDelete>
        where TEntity : ICreationAudited<TEntityKey, TUserKey>
        where TRegister : ICommandMapped<TEntity, TEntityKey>
        where TUpdate : ICommandMappedWithId<TEntity, TEntityKey>
        where TDelete : ICommandWithId<TEntityKey>
    {
        protected CreationAuditedCommandHandler(
            DomainNotificationContext domainNotificationContext,
            CommonMessages commonMessages,
            IMapper mapper,
            IUnitOfWork uow,
            ICreationAuditedRepository<TEntity, TEntityKey, TUserKey> repository) : base(domainNotificationContext, commonMessages, mapper, uow, repository)
        {
        }
    }

    public abstract class CreationAuditedCommandHandlerBase<TEntity, TRegister, TUpdate, TDelete> :
        CreationAuditedCommandHandler<TEntity, int, int, TRegister, TUpdate, TDelete>
        where TEntity : ICreationAudited<int, int>
        where TRegister : ICommandMapped<TEntity, int>
        where TUpdate : ICommandMappedWithId<TEntity, int>
        where TDelete : ICommandWithId<int>
    {
        protected CreationAuditedCommandHandlerBase(
            DomainNotificationContext domainNotificationContext,
            CommonMessages commonMessages,
            IMapper mapper,
            IUnitOfWork uow,
            ICreationAuditedRepository<TEntity, int, int> repository) : base(domainNotificationContext, commonMessages, mapper, uow, repository)
        {
        }
    }
}