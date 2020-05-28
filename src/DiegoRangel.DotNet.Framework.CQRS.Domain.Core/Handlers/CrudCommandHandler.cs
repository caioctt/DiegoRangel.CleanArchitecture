using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Commands;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Interfaces;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Messages;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.NotificationPattern;
using MediatR;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Handlers
{
    public abstract class CrudCommandHandler<TEntity, TPrimaryKey, TDelete> :
        CommandHandlerBase,
        ICommandHandler<TDelete>
        where TEntity : IEntity<TPrimaryKey>
        where TDelete : ICommandWithId<TPrimaryKey>
    {
        private readonly ICrudRepository<TEntity, TPrimaryKey> _repository;
        private readonly CommonMessages _commonMessages;

        protected CrudCommandHandler(
            NotificationContext domainNotificationContext, 
            CommonMessages commonMessages,
            IUnitOfWork uow,
            ICrudRepository<TEntity, TPrimaryKey> repository) : base(domainNotificationContext, commonMessages, uow)
        {
            _repository = repository;
            _commonMessages = commonMessages;
        }

        public virtual async Task<Unit> Handle(TDelete request, CancellationToken cancellationToken)
        {
            var entity = await _repository.FindByIdAsync(request.Id);
            if (entity == null)
                return Fail(_commonMessages.NotFound ?? "Not found");

            await _repository.DeleteAsync(entity);
            await Commit();

            return Finish();
        }
    }

    public abstract class CrudCommandHandler<TEntity, TPrimaryKey, TUpdate, TDelete> :
        CrudCommandHandler<TEntity, TPrimaryKey, TDelete>,
        ICommandHandler<TUpdate, TEntity>
        where TEntity : class, IEntity<TPrimaryKey>
        where TUpdate : ICommandMappedWithId<TEntity, TPrimaryKey, TEntity>
        where TDelete : ICommandWithId<TPrimaryKey>
    {
        private readonly IMapper _mapper;
        private readonly ICrudRepository<TEntity, TPrimaryKey> _repository;
        private readonly CommonMessages _commonMessages;

        protected CrudCommandHandler(
            NotificationContext domainNotificationContext,
            CommonMessages commonMessages,
            IMapper mapper,
            IUnitOfWork uow,
            ICrudRepository<TEntity, TPrimaryKey> repository) : base(domainNotificationContext, commonMessages, uow, repository)
        {
            _mapper = mapper;
            _repository = repository;
            _commonMessages = commonMessages;
        }

        public virtual async Task<TEntity> Handle(TUpdate request, CancellationToken cancellationToken)
        {
            var entity = await _repository.FindByIdAsync(request.Id);
            if (entity == null)
                return Fail<TEntity>(_commonMessages.NotFound ?? "Not found", null);

            _mapper.Map(request, entity);

            if (!IsValid<TEntity, TPrimaryKey>(entity)) 
                return Fail<TEntity>(null);

            await _repository.UpdateAsync(entity);
            await Commit();

            return entity;
        }
    }

    public abstract class CrudCommandHandler<TEntity, TPrimaryKey, TRegister, TUpdate, TDelete> :
        CrudCommandHandler<TEntity, TPrimaryKey, TUpdate, TDelete>,
        ICommandHandler<TRegister, TEntity>
        where TEntity : class, IEntity<TPrimaryKey>
        where TRegister : ICommandMapped<TEntity, TPrimaryKey, TEntity>
        where TUpdate : ICommandMappedWithId<TEntity, TPrimaryKey, TEntity>
        where TDelete : ICommandWithId<TPrimaryKey>
    {
        private readonly IMapper _mapper;
        private readonly ICrudRepository<TEntity, TPrimaryKey> _repository;

        protected CrudCommandHandler(
            NotificationContext domainNotificationContext,
            CommonMessages commonMessages,
            IMapper mapper,
            IUnitOfWork uow,
            ICrudRepository<TEntity, TPrimaryKey> repository) : base(domainNotificationContext, commonMessages, mapper, uow, repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public virtual async Task<TEntity> Handle(TRegister request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<TEntity>(request);

            if (!IsValid<TEntity, TPrimaryKey>(entity))
                return Fail<TEntity>(null);

            await _repository.AddAsync(entity);
            await Commit();

            return entity;
        }
    }

    public abstract class CrudCommandHandlerBase<TEntity, TRegister, TUpdate, TDelete> :
        CrudCommandHandler<TEntity, int, TRegister, TUpdate, TDelete>
        where TEntity : class, IEntity
        where TRegister : ICommandMapped<TEntity, int, TEntity>
        where TUpdate : ICommandMappedWithId<TEntity, int, TEntity>
        where TDelete : ICommandWithId
    {
        protected CrudCommandHandlerBase(
            NotificationContext domainNotificationContext,
            CommonMessages commonMessages,
            IMapper mapper,
            IUnitOfWork uow,
            ICrudRepository<TEntity, int> repository) : base(domainNotificationContext, commonMessages, mapper, uow, repository)
        {
        }
    }
}