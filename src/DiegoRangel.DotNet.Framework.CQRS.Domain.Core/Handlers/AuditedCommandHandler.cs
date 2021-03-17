using AutoMapper;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Commands;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Interfaces;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories.Agregations;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Messages;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.NotificationPattern;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Handlers
{
    public abstract class AuditedCommandHandler<TEntity, TEntityKey, TUserKey, TDeleteCommand, TUnitOfWork> :
        CreationAuditedCommandHandler<TEntity, TEntityKey, TUserKey, TDeleteCommand, TUnitOfWork>
        where TEntity : class, IAudited<TEntityKey, TUserKey>
        where TDeleteCommand : ICommandWithId<TEntityKey>
        where TUnitOfWork : IUnitOfWork
    {
        protected AuditedCommandHandler(
            INotificationContext notificationContext,
            CommonMessages commonMessages,
            TUnitOfWork uow,
            IAuditedRepository<TEntity, TEntityKey, TUserKey> repository) : base(notificationContext, commonMessages, uow, repository)
        {
        }
    }

    public abstract class AuditedCommandHandler<TEntity, TEntityKey, TUserKey, TUpdateCommand, TDeleteCommand, TUnitOfWork> :
        CreationAuditedCommandHandler<TEntity, TEntityKey, TUserKey, TUpdateCommand, TDeleteCommand, TUnitOfWork>
        where TEntity : class, IAudited<TEntityKey, TUserKey>
        where TUpdateCommand : ICommandMappedWithId<TEntity, TEntityKey, TEntity>
        where TDeleteCommand : ICommandWithId<TEntityKey>
        where TUnitOfWork : IUnitOfWork
    {
        protected AuditedCommandHandler(
            INotificationContext notificationContext,
            CommonMessages commonMessages,
            IMapper mapper,
            TUnitOfWork uow,
            IAuditedRepository<TEntity, TEntityKey, TUserKey> repository) : base(notificationContext, commonMessages, mapper, uow, repository)
        {
        }
    }

    public abstract class AuditedCommandHandler<TEntity, TEntityKey, TUserKey, TRegisterCommand, TUpdateCommand, TDeleteCommand, TUnitOfWork> :
        CreationAuditedCommandHandler<TEntity, TEntityKey, TUserKey, TRegisterCommand, TUpdateCommand, TDeleteCommand, TUnitOfWork>
        where TEntity : class, IAudited<TEntityKey, TUserKey>
        where TRegisterCommand : ICommandMapped<TEntity, TEntityKey, TEntity>
        where TUpdateCommand : ICommandMappedWithId<TEntity, TEntityKey, TEntity>
        where TDeleteCommand : ICommandWithId<TEntityKey>
        where TUnitOfWork : IUnitOfWork
    {
        protected AuditedCommandHandler(
            INotificationContext notificationContext,
            CommonMessages commonMessages,
            IMapper mapper,
            TUnitOfWork uow,
            IAuditedRepository<TEntity, TEntityKey, TUserKey> repository) : base(notificationContext, commonMessages, mapper, uow, repository)
        {
        }
    }

    public abstract class AuditedCommandHandlerBase<TEntity, TRegisterCommand, TUpdateCommand, TDeleteCommand, TUnitOfWork> :
        AuditedCommandHandler<TEntity, int, int, TRegisterCommand, TUpdateCommand, TDeleteCommand, TUnitOfWork>
        where TEntity : class, IAudited<int, int>
        where TRegisterCommand : ICommandMapped<TEntity, int, TEntity>
        where TUpdateCommand : ICommandMappedWithId<TEntity, int, TEntity>
        where TDeleteCommand : ICommandWithId<int>
        where TUnitOfWork : IUnitOfWork
    {
        protected AuditedCommandHandlerBase(
            INotificationContext notificationContext,
            CommonMessages commonMessages,
            IMapper mapper,
            TUnitOfWork uow,
            IAuditedRepository<TEntity, int, int> repository) : base(notificationContext, commonMessages, mapper, uow, repository)
        {
        }
    }
}