using AutoMapper;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Commands;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Interfaces;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories.Agregations;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Messages;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.NotificationPattern;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Handlers
{
    public abstract class CreationAuditedCommandHandler<TEntity, TEntityKey, TUserKey, TDeleteCommand, TUnitOfWork> :
        CrudCommandHandler<TEntity, TEntityKey, TDeleteCommand, TUnitOfWork>
        where TEntity : ICreationAudited<TEntityKey, TUserKey>
        where TDeleteCommand : ICommandWithId<TEntityKey>
        where TUnitOfWork : IUnitOfWork
    {
        protected CreationAuditedCommandHandler(
            INotificationContext notificationContext, 
            CommonMessages commonMessages,
            TUnitOfWork uow,
            ICreationAuditedRepository<TEntity, TEntityKey, TUserKey> repository) : base(notificationContext, commonMessages, uow, repository)
        {
        }
    }

    public abstract class CreationAuditedCommandHandler<TEntity, TEntityKey, TUserKey, TUpdateCommand, TDeleteCommand, TUnitOfWork> :
        CrudCommandHandler<TEntity, TEntityKey, TUpdateCommand, TDeleteCommand, TUnitOfWork>
        where TEntity : class, ICreationAudited<TEntityKey, TUserKey>
        where TUpdateCommand : ICommandMappedWithId<TEntity, TEntityKey, TEntity>
        where TDeleteCommand : ICommandWithId<TEntityKey>
        where TUnitOfWork : IUnitOfWork
    {
        protected CreationAuditedCommandHandler(
            INotificationContext notificationContext,
            CommonMessages commonMessages,
            IMapper mapper,
            TUnitOfWork uow,
            ICreationAuditedRepository<TEntity, TEntityKey, TUserKey> repository) : base(notificationContext, commonMessages, mapper, uow, repository)
        {
        }
    }

    public abstract class CreationAuditedCommandHandler<TEntity, TEntityKey, TUserKey, TRegisterCommand, TUpdateCommand, TDeleteCommand, TUnitOfWork> :
        CrudCommandHandler<TEntity, TEntityKey, TRegisterCommand, TUpdateCommand, TDeleteCommand, TUnitOfWork>
        where TEntity : class, ICreationAudited<TEntityKey, TUserKey>
        where TRegisterCommand : ICommandMapped<TEntity, TEntityKey, TEntity>
        where TUpdateCommand : ICommandMappedWithId<TEntity, TEntityKey, TEntity>
        where TDeleteCommand : ICommandWithId<TEntityKey>
        where TUnitOfWork : IUnitOfWork
    {
        protected CreationAuditedCommandHandler(
            INotificationContext notificationContext,
            CommonMessages commonMessages,
            IMapper mapper,
            TUnitOfWork uow,
            ICreationAuditedRepository<TEntity, TEntityKey, TUserKey> repository) : base(notificationContext, commonMessages, mapper, uow, repository)
        {
        }
    }

    public abstract class CreationAuditedCommandHandlerBase<TEntity, TRegisterCommand, TUpdateCommand, TDeleteCommand, TUnitOfWork> :
        CreationAuditedCommandHandler<TEntity, int, int, TRegisterCommand, TUpdateCommand, TDeleteCommand, TUnitOfWork>
        where TEntity : class, ICreationAudited<int, int>
        where TRegisterCommand : ICommandMapped<TEntity, int, TEntity>
        where TUpdateCommand : ICommandMappedWithId<TEntity, int, TEntity>
        where TDeleteCommand : ICommandWithId<int>
        where TUnitOfWork : IUnitOfWork
    {
        protected CreationAuditedCommandHandlerBase(
            INotificationContext notificationContext,
            CommonMessages commonMessages,
            IMapper mapper,
            TUnitOfWork uow,
            ICreationAuditedRepository<TEntity, int, int> repository) : base(notificationContext, commonMessages, mapper, uow, repository)
        {
        }
    }
}