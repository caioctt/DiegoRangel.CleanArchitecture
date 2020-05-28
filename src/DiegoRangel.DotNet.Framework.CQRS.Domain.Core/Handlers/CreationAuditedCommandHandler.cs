using AutoMapper;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Commands;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Interfaces;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories.Agregations;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Messages;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.NotificationPattern;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Handlers
{
    public abstract class CreationAuditedCommandHandler<TEntity, TEntityKey, TUserKey, TDelete> :
        CrudCommandHandler<TEntity, TEntityKey, TDelete>
        where TEntity : ICreationAudited<TEntityKey, TUserKey>
        where TDelete : ICommandWithId<TEntityKey>
        where TUserKey : struct
    {
        protected CreationAuditedCommandHandler(
            NotificationContext domainNotificationContext, 
            CommonMessages commonMessages, 
            IUnitOfWork uow,
            ICreationAuditedRepository<TEntity, TEntityKey, TUserKey> repository) : base(domainNotificationContext, commonMessages, uow, repository)
        {
        }
    }

    public abstract class CreationAuditedCommandHandler<TEntity, TEntityKey, TUserKey, TUpdate, TDelete> :
        CrudCommandHandler<TEntity, TEntityKey, TUpdate, TDelete>
        where TEntity : class, ICreationAudited<TEntityKey, TUserKey>
        where TUpdate : ICommandMappedWithId<TEntity, TEntityKey, TEntity>
        where TDelete : ICommandWithId<TEntityKey>
        where TUserKey : struct
    {
        protected CreationAuditedCommandHandler(
            NotificationContext domainNotificationContext,
            CommonMessages commonMessages,
            IMapper mapper,
            IUnitOfWork uow,
            ICreationAuditedRepository<TEntity, TEntityKey, TUserKey> repository) : base(domainNotificationContext, commonMessages, mapper, uow, repository)
        {
        }
    }

    public abstract class CreationAuditedCommandHandler<TEntity, TEntityKey, TUserKey, TRegister, TUpdate, TDelete> :
        CrudCommandHandler<TEntity, TEntityKey, TRegister, TUpdate, TDelete>
        where TEntity : class, ICreationAudited<TEntityKey, TUserKey>
        where TRegister : ICommandMapped<TEntity, TEntityKey, TEntity>
        where TUpdate : ICommandMappedWithId<TEntity, TEntityKey, TEntity>
        where TDelete : ICommandWithId<TEntityKey>
        where TUserKey : struct
    {
        protected CreationAuditedCommandHandler(
            NotificationContext domainNotificationContext,
            CommonMessages commonMessages,
            IMapper mapper,
            IUnitOfWork uow,
            ICreationAuditedRepository<TEntity, TEntityKey, TUserKey> repository) : base(domainNotificationContext, commonMessages, mapper, uow, repository)
        {
        }
    }

    public abstract class CreationAuditedCommandHandlerBase<TEntity, TRegister, TUpdate, TDelete> :
        CreationAuditedCommandHandler<TEntity, int, int, TRegister, TUpdate, TDelete>
        where TEntity : class, ICreationAudited<int, int>
        where TRegister : ICommandMapped<TEntity, int, TEntity>
        where TUpdate : ICommandMappedWithId<TEntity, int, TEntity>
        where TDelete : ICommandWithId<int>
    {
        protected CreationAuditedCommandHandlerBase(
            NotificationContext domainNotificationContext,
            CommonMessages commonMessages,
            IMapper mapper,
            IUnitOfWork uow,
            ICreationAuditedRepository<TEntity, int, int> repository) : base(domainNotificationContext, commonMessages, mapper, uow, repository)
        {
        }
    }
}