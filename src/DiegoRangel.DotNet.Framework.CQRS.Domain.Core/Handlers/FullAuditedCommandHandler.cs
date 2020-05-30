using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Auditing;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Commands;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Interfaces;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories.Agregations;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Messages;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.NotificationPattern;
using MediatR;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Handlers
{
    public abstract class FullAuditedCommandHandler<TEntity, TEntityKey, TUserKey, TDeleteCommand, TUnitOfWork> :
        AuditedCommandHandler<TEntity, TEntityKey, TUserKey, TDeleteCommand, TUnitOfWork>
        where TEntity : class, IFullAudited<TEntityKey, TUserKey>
        where TDeleteCommand : ICommandWithId<TEntityKey>
        where TUserKey : struct
        where TUnitOfWork : IUnitOfWork
    {
        private readonly IFullAuditedRepository<TEntity, TEntityKey, TUserKey> _repository;
        private readonly CommonMessages _commonMessages;

        protected FullAuditedCommandHandler(
            NotificationContext domainNotificationContext,
            CommonMessages commonMessages,
            TUnitOfWork uow,
            IFullAuditedRepository<TEntity, TEntityKey, TUserKey> repository) : base(domainNotificationContext, commonMessages, uow, repository)
        {
            _repository = repository;
            _commonMessages = commonMessages;
        }

        public override async Task<Unit> Handle(TDeleteCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.FindByIdAsync(request.Id);
            if (entity == null)
                return Fail(_commonMessages.NotFound ?? "Not found");

            await _repository.MoveToTrashAsync(entity);
            await Commit();

            return Finish();
        }
    }

    public abstract class FullAuditedCommandHandler<TEntity, TEntityKey, TUserKey, TUpdateCommand, TDeleteCommand, TUnitOfWork> :
        AuditedCommandHandler<TEntity, TEntityKey, TUserKey, TUpdateCommand, TDeleteCommand, TUnitOfWork>
        where TEntity : class, IFullAudited<TEntityKey, TUserKey>
        where TUpdateCommand : ICommandMappedWithId<TEntity, TEntityKey, TEntity>
        where TDeleteCommand : ICommandWithId<TEntityKey>
        where TUserKey : struct
        where TUnitOfWork : IUnitOfWork
    {
        protected FullAuditedCommandHandler(
            NotificationContext domainNotificationContext,
            CommonMessages commonMessages,
            IMapper mapper,
            TUnitOfWork uow,
            IFullAuditedRepository<TEntity, TEntityKey, TUserKey> repository) : base(domainNotificationContext, commonMessages, mapper, uow, repository)
        {
        }
    }

    public abstract class FullAuditedCommandHandler<TEntity, TEntityKey, TUserKey, TRegisterCommand, TUpdateCommand, TDeleteCommand, TUnitOfWork> :
        AuditedCommandHandler<TEntity, TEntityKey, TUserKey, TRegisterCommand, TUpdateCommand, TDeleteCommand, TUnitOfWork>
        where TEntity : class, IFullAudited<TEntityKey, TUserKey>
        where TRegisterCommand : ICommandMapped<TEntity, TEntityKey, TEntity>
        where TUpdateCommand : ICommandMappedWithId<TEntity, TEntityKey, TEntity>
        where TDeleteCommand : ICommandWithId<TEntityKey>
        where TUserKey : struct
        where TUnitOfWork : IUnitOfWork
    {
        protected FullAuditedCommandHandler(
            NotificationContext domainNotificationContext,
            CommonMessages commonMessages,
            IMapper mapper,
            TUnitOfWork uow,
            IFullAuditedRepository<TEntity, TEntityKey, TUserKey> repository) : base(domainNotificationContext, commonMessages, mapper, uow, repository)
        {
        }
    }

    public abstract class FullAuditedCommandHandlerBase<TEntity, TRegisterCommand, TUpdateCommand, TDeleteCommand, TUnitOfWork> :
        FullAuditedCommandHandler<TEntity, int, int, TRegisterCommand, TUpdateCommand, TDeleteCommand, TUnitOfWork>
        where TEntity : class, IFullAudited<int, int>
        where TRegisterCommand : ICommandMapped<TEntity, int, TEntity>
        where TUpdateCommand : ICommandMappedWithId<TEntity, int, TEntity>
        where TDeleteCommand : ICommandWithId<int>
        where TUnitOfWork : IUnitOfWork
    {
        protected FullAuditedCommandHandlerBase(
            NotificationContext domainNotificationContext,
            CommonMessages commonMessages,
            IMapper mapper,
            TUnitOfWork uow,
            IFullAuditedRepository<TEntity, int, int> repository) : base(domainNotificationContext, commonMessages, mapper, uow, repository)
        {
        }
    }
}