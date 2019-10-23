using System.Threading;
using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Commands;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Interfaces;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Notifications;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Responses;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Handlers
{
    public abstract class CrudSoftDeletableCommandHandler<TEntity, TPrimaryKey, TDelete> :
        CommandHandlerBase,
        ICommandHandler<TDelete>
        where TPrimaryKey : struct
        where TEntity : IEntity<TPrimaryKey>, ISoftDelete
        where TDelete : ICommandWithId<TPrimaryKey>
    {
        private readonly ICrudSoftDeletableRepository<TEntity, TPrimaryKey> _repository;
        private readonly DomainNotificationContext _domainNotificationContext;

        protected CrudSoftDeletableCommandHandler(
            DomainNotificationContext domainNotificationContext,
            IUnitOfWork uow,
            ICrudSoftDeletableRepository<TEntity, TPrimaryKey> repository) : base(domainNotificationContext, uow)
        {
            _repository = repository;
            _domainNotificationContext = domainNotificationContext;
        }

        public virtual async Task<IResponse> Handle(TDelete request, CancellationToken cancellationToken)
        {
            var entity = await _repository.FindById(request.Id);
            if (entity == null || entity.IsDeleted)
            {
                _domainNotificationContext.AddNotification("Not found");
                return Fail();
            }

            await _repository.Delete(entity);

            if (await Commit())
                return NoContent();
            return Fail();
        }
    }

    public abstract class CrudSoftDeletableCommandHandler<TEntity, TPrimaryKey, TUpdate, TDelete> :
        CrudSoftDeletableCommandHandler<TEntity, TPrimaryKey, TDelete>,
        ICommandHandler<TUpdate>
        where TPrimaryKey : struct
        where TEntity : IEntity<TPrimaryKey>, ISoftDelete
        where TUpdate : ICommandMappedWithId<TEntity, TPrimaryKey>
        where TDelete : ICommandWithId<TPrimaryKey>
    {
        private readonly IMapper _mapper;
        private readonly ICrudSoftDeletableRepository<TEntity, TPrimaryKey> _repository;
        private readonly DomainNotificationContext _domainNotificationContext;

        protected CrudSoftDeletableCommandHandler(
            DomainNotificationContext domainNotificationContext,
            IUnitOfWork uow,
            IMapper mapper,
            ICrudSoftDeletableRepository<TEntity, TPrimaryKey> repository) : base(domainNotificationContext, uow, repository)
        {
            _mapper = mapper;
            _repository = repository;
            _domainNotificationContext = domainNotificationContext;
        }

        public virtual async Task<IResponse> Handle(TUpdate request, CancellationToken cancellationToken)
        {
            var entity = await _repository.FindById(request.Id);
            if (entity == null || entity.IsDeleted)
            {
                _domainNotificationContext.AddNotification("Not found");
                return Fail();
            }

            _mapper.Map(request, entity);

            if (!IsValid<TEntity, TPrimaryKey>(entity)) return Fail();

            await _repository.Update(entity);

            if (await Commit())
                return Ok(entity);
            return Fail();
        }
    }

    public abstract class CrudSoftDeletableCommandHandler<TEntity, TPrimaryKey, TRegister, TUpdate, TDelete> :
        CrudSoftDeletableCommandHandler<TEntity, TPrimaryKey, TUpdate, TDelete>,
        ICommandHandler<TRegister>
        where TPrimaryKey : struct
        where TEntity : IEntity<TPrimaryKey>, ISoftDelete
        where TRegister : ICommandMapped<TEntity, TPrimaryKey>
        where TUpdate : ICommandMappedWithId<TEntity, TPrimaryKey>
        where TDelete : ICommandWithId<TPrimaryKey>
    {
        private readonly IMapper _mapper;
        private readonly ICrudSoftDeletableRepository<TEntity, TPrimaryKey> _repository;

        protected CrudSoftDeletableCommandHandler(
            DomainNotificationContext domainNotificationContext,
            IUnitOfWork uow,
            IMapper mapper,
            ICrudSoftDeletableRepository<TEntity, TPrimaryKey> repository) : base(domainNotificationContext, uow, mapper, repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public virtual async Task<IResponse> Handle(TRegister request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<TEntity>(request);

            if (!IsValid<TEntity, TPrimaryKey>(entity))
                return Fail();

            await _repository.Add(entity);

            if (await Commit())
                return Ok(entity);
            return Fail();
        }
    }

    public abstract class CrudSoftDeletableCommandHandlerBase<TEntity, TRegister, TUpdate, TDelete> :
        CrudSoftDeletableCommandHandler<TEntity, int, TRegister, TUpdate, TDelete>
        where TEntity : IEntity, ISoftDelete
        where TRegister : ICommandMapped<TEntity>
        where TUpdate : ICommandMappedWithId<TEntity>
        where TDelete : ICommandWithId
    {
        protected CrudSoftDeletableCommandHandlerBase(
            DomainNotificationContext domainNotificationContext,
            IUnitOfWork uow,
            IMapper mapper,
            ICrudSoftDeletableRepository<TEntity> repository) : base(domainNotificationContext, uow, mapper, repository)
        {
        }
    }
}