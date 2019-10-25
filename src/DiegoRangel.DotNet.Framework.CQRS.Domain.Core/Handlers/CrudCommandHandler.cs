using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Commands;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Interfaces;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Notifications;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Responses;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Messages;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Handlers
{
    public abstract class CrudCommandHandler<TEntity, TPrimaryKey, TDelete> :
        CommandHandlerBase,
        ICommandHandler<TDelete>
        where TPrimaryKey : struct
        where TEntity : IEntity<TPrimaryKey>
        where TDelete : ICommandWithId<TPrimaryKey>
    {
        private readonly ICrudRepository<TEntity, TPrimaryKey> _repository;
        private readonly DomainNotificationContext _domainNotificationContext;
        private readonly CommonMessages _commonMessages;

        protected CrudCommandHandler(
            DomainNotificationContext domainNotificationContext, 
            CommonMessages commonMessages,
            IUnitOfWork uow,
            ICrudRepository<TEntity, TPrimaryKey> repository) : base(domainNotificationContext, commonMessages, uow)
        {
            _repository = repository;
            _commonMessages = commonMessages;
            _domainNotificationContext = domainNotificationContext;
        }

        public virtual async Task<IResponse> Handle(TDelete request, CancellationToken cancellationToken)
        {
            var entity = await _repository.FindByIdAsync(request.Id);
            if (entity == null)
            {
                _domainNotificationContext.AddNotification(_commonMessages.NotFound ?? "Not found");
                return Fail();
            }

            await _repository.DeleteAsync(entity);

            if (await Commit())
                return NoContent();
            return Fail();
        }
    }

    public abstract class CrudCommandHandler<TEntity, TPrimaryKey, TUpdate, TDelete> :
        CrudCommandHandler<TEntity, TPrimaryKey, TDelete>,
        ICommandHandler<TUpdate>
        where TPrimaryKey : struct
        where TEntity : IEntity<TPrimaryKey>
        where TUpdate : ICommandMappedWithId<TEntity, TPrimaryKey>
        where TDelete : ICommandWithId<TPrimaryKey>
    {
        private readonly IMapper _mapper;
        private readonly ICrudRepository<TEntity, TPrimaryKey> _repository;
        private readonly DomainNotificationContext _domainNotificationContext;
        private readonly CommonMessages _commonMessages;

        protected CrudCommandHandler(
            DomainNotificationContext domainNotificationContext,
            CommonMessages commonMessages,
            IUnitOfWork uow,
            IMapper mapper,
            ICrudRepository<TEntity, TPrimaryKey> repository) : base(domainNotificationContext, commonMessages, uow, repository)
        {
            _mapper = mapper;
            _repository = repository;
            _domainNotificationContext = domainNotificationContext;
            _commonMessages = commonMessages;
        }

        public virtual async Task<IResponse> Handle(TUpdate request, CancellationToken cancellationToken)
        {
            var entity = await _repository.FindByIdAsync(request.Id);
            if (entity == null)
            {
                _domainNotificationContext.AddNotification(_commonMessages.NotFound ?? "Not found");
                return Fail();
            }

            _mapper.Map(request, entity);

            if (!IsValid<TEntity, TPrimaryKey>(entity)) return Fail();

            await _repository.UpdateAsync(entity);

            if (await Commit())
                return Ok(entity);
            return Fail();
        }
    }

    public abstract class CrudCommandHandler<TEntity, TPrimaryKey, TRegister, TUpdate, TDelete> :
        CrudCommandHandler<TEntity, TPrimaryKey, TUpdate, TDelete>,
        ICommandHandler<TRegister>
        where TPrimaryKey : struct
        where TEntity : IEntity<TPrimaryKey>
        where TRegister : ICommandMapped<TEntity, TPrimaryKey>
        where TUpdate : ICommandMappedWithId<TEntity, TPrimaryKey>
        where TDelete : ICommandWithId<TPrimaryKey>
    {
        private readonly IMapper _mapper;
        private readonly ICrudRepository<TEntity, TPrimaryKey> _repository;

        protected CrudCommandHandler(
            DomainNotificationContext domainNotificationContext,
            CommonMessages commonMessages,
            IUnitOfWork uow,
            IMapper mapper,
            ICrudRepository<TEntity, TPrimaryKey> repository) : base(domainNotificationContext, commonMessages, uow, mapper, repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public virtual async Task<IResponse> Handle(TRegister request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<TEntity>(request);

            if (!IsValid<TEntity, TPrimaryKey>(entity))
                return Fail();

            await _repository.AddAsync(entity);

            if (await Commit())
                return Ok(entity);
            return Fail();
        }
    }

    public abstract class CrudCommandHandlerBase<TEntity, TRegister, TUpdate, TDelete> :
        CrudCommandHandler<TEntity, int, TRegister, TUpdate, TDelete>
        where TEntity : IEntity
        where TRegister : ICommandMapped<TEntity>
        where TUpdate : ICommandMappedWithId<TEntity>
        where TDelete : ICommandWithId
    {
        protected CrudCommandHandlerBase(
            DomainNotificationContext domainNotificationContext,
            CommonMessages commonMessages,
            IUnitOfWork uow,
            IMapper mapper,
            ICrudRepository<TEntity, int> repository) : base(domainNotificationContext, commonMessages, uow, mapper, repository)
        {
        }
    }
}