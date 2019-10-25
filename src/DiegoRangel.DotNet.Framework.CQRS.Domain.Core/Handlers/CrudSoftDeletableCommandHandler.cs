using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Commands;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Interfaces;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Notifications;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Responses;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Handlers
{
    public abstract class CrudSoftDeletableCommandHandler<TEntity, TPrimaryKey, TDelete> :
        CrudCommandHandler<TEntity, TPrimaryKey, TDelete>
        where TPrimaryKey : struct
        where TEntity : IEntity<TPrimaryKey>, ISoftDelete
        where TDelete : ICommandWithId<TPrimaryKey>
    {
        private readonly ICrudSoftDeletableRepository<TEntity, TPrimaryKey> _repository;
        private readonly DomainNotificationContext _domainNotificationContext;

        protected CrudSoftDeletableCommandHandler(
            DomainNotificationContext domainNotificationContext,
            IUnitOfWork uow,
            ICrudSoftDeletableRepository<TEntity, TPrimaryKey> repository) : base(domainNotificationContext, uow, repository)
        {
            _repository = repository;
            _domainNotificationContext = domainNotificationContext;
        }

        public override async Task<IResponse> Handle(TDelete request, CancellationToken cancellationToken)
        {
            var entity = await _repository.FindByIdAsync(request.Id);
            if (entity == null)
            {
                _domainNotificationContext.AddNotification("Not found");
                return Fail();
            }

            await _repository.MoveToTrashAsync(entity);

            if (await Commit())
                return NoContent();
            return Fail();
        }
    }

    public abstract class CrudSoftDeletableCommandHandler<TEntity, TPrimaryKey, TUpdate, TDelete> :
        CrudCommandHandler<TEntity, TPrimaryKey, TUpdate, TDelete>
        where TPrimaryKey : struct
        where TEntity : IEntity<TPrimaryKey>, ISoftDelete
        where TUpdate : ICommandMappedWithId<TEntity, TPrimaryKey>
        where TDelete : ICommandWithId<TPrimaryKey>
    {
        protected CrudSoftDeletableCommandHandler(
            DomainNotificationContext domainNotificationContext,
            IUnitOfWork uow, 
            IMapper mapper,
            ICrudSoftDeletableRepository<TEntity, TPrimaryKey> repository) : base(domainNotificationContext, uow, mapper, repository)
        {
        }
    }

    public abstract class CrudSoftDeletableCommandHandler<TEntity, TPrimaryKey, TRegister, TUpdate, TDelete> :
        CrudCommandHandler<TEntity, TPrimaryKey, TRegister, TUpdate, TDelete>
        where TPrimaryKey : struct
        where TEntity : IEntity<TPrimaryKey>, ISoftDelete
        where TRegister : ICommandMapped<TEntity, TPrimaryKey>
        where TUpdate : ICommandMappedWithId<TEntity, TPrimaryKey>
        where TDelete : ICommandWithId<TPrimaryKey>
    {
        protected CrudSoftDeletableCommandHandler(
            DomainNotificationContext domainNotificationContext,
            IUnitOfWork uow,
            IMapper mapper,
            ICrudSoftDeletableRepository<TEntity, TPrimaryKey> repository) : base(domainNotificationContext, uow, mapper, repository)
        {
        }
    }

    public abstract class CrudSoftDeletableCommandHandlerBase<TEntity, TRegister, TUpdate, TDelete> :
        CrudCommandHandler<TEntity, int, TRegister, TUpdate, TDelete>
        where TEntity : IEntity<int>, ISoftDelete
        where TRegister : ICommandMapped<TEntity, int>
        where TUpdate : ICommandMappedWithId<TEntity, int>
        where TDelete : ICommandWithId<int>
    {
        protected CrudSoftDeletableCommandHandlerBase(
            DomainNotificationContext domainNotificationContext,
            IUnitOfWork uow,
            IMapper mapper,
            ICrudSoftDeletableRepository<TEntity, int> repository) : base(domainNotificationContext, uow, mapper, repository)
        {
        }
    }
}