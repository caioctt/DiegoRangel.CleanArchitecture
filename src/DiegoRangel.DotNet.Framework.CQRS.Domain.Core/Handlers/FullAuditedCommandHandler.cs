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
    public abstract class FullAuditedCommandHandler<TEntity, TEntityKey, TUserKey, TDelete> :
        AuditedCommandHandler<TEntity, TEntityKey, TUserKey, TDelete>
        where TEntity : class, IFullAudited<TEntityKey, TUserKey>
        where TDelete : ICommandWithId<TEntityKey>
        where TUserKey : struct
    {
        private readonly IFullAuditedRepository<TEntity, TEntityKey, TUserKey> _repository;
        private readonly CommonMessages _commonMessages;

        protected FullAuditedCommandHandler(
            NotificationContext domainNotificationContext,
            CommonMessages commonMessages,
            IUnitOfWork uow,
            IFullAuditedRepository<TEntity, TEntityKey, TUserKey> repository) : base(domainNotificationContext, commonMessages, uow, repository)
        {
            _repository = repository;
            _commonMessages = commonMessages;
        }

        public override async Task<Unit> Handle(TDelete request, CancellationToken cancellationToken)
        {
            var entity = await _repository.FindByIdAsync(request.Id);
            if (entity == null)
                return Fail(_commonMessages.NotFound ?? "Not found");

            await _repository.MoveToTrashAsync(entity);
            await Commit();

            return Finish();
        }
    }

    public abstract class FullAuditedCommandHandler<TEntity, TEntityKey, TUserKey, TUpdate, TDelete> :
        AuditedCommandHandler<TEntity, TEntityKey, TUserKey, TUpdate, TDelete>
        where TEntity : class, IFullAudited<TEntityKey, TUserKey>
        where TUpdate : ICommandMappedWithId<TEntity, TEntityKey, TEntity>
        where TDelete : ICommandWithId<TEntityKey>
        where TUserKey : struct
    {
        protected FullAuditedCommandHandler(
            NotificationContext domainNotificationContext,
            CommonMessages commonMessages,
            IMapper mapper,
            IUnitOfWork uow,
            IFullAuditedRepository<TEntity, TEntityKey, TUserKey> repository) : base(domainNotificationContext, commonMessages, mapper, uow, repository)
        {
        }
    }

    public abstract class FullAuditedCommandHandler<TEntity, TEntityKey, TUserKey, TRegister, TUpdate, TDelete> :
        AuditedCommandHandler<TEntity, TEntityKey, TUserKey, TRegister, TUpdate, TDelete>
        where TEntity : class, IFullAudited<TEntityKey, TUserKey>
        where TRegister : ICommandMapped<TEntity, TEntityKey, TEntity>
        where TUpdate : ICommandMappedWithId<TEntity, TEntityKey, TEntity>
        where TDelete : ICommandWithId<TEntityKey>
        where TUserKey : struct
    {
        protected FullAuditedCommandHandler(
            NotificationContext domainNotificationContext,
            CommonMessages commonMessages,
            IMapper mapper,
            IUnitOfWork uow,
            IFullAuditedRepository<TEntity, TEntityKey, TUserKey> repository) : base(domainNotificationContext, commonMessages, mapper, uow, repository)
        {
        }
    }

    public abstract class FullAuditedCommandHandlerBase<TEntity, TRegister, TUpdate, TDelete> :
        FullAuditedCommandHandler<TEntity, int, int, TRegister, TUpdate, TDelete>
        where TEntity : class, IFullAudited<int, int>
        where TRegister : ICommandMapped<TEntity, int, TEntity>
        where TUpdate : ICommandMappedWithId<TEntity, int, TEntity>
        where TDelete : ICommandWithId<int>
    {
        protected FullAuditedCommandHandlerBase(
            NotificationContext domainNotificationContext,
            CommonMessages commonMessages,
            IMapper mapper,
            IUnitOfWork uow,
            IFullAuditedRepository<TEntity, int, int> repository) : base(domainNotificationContext, commonMessages, mapper, uow, repository)
        {
        }
    }
}