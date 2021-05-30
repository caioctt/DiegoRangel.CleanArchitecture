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
    public abstract class CrudCommandHandler<TEntity, TPrimaryKey, TDeleteCommand, TUnitOfWork> :
        CommandHandlerBase<TUnitOfWork>,
        ICommandHandler<TDeleteCommand>
        where TEntity : IEntity<TPrimaryKey>
        where TDeleteCommand : ICommandWithId<TPrimaryKey>
        where TUnitOfWork : IUnitOfWork
    {
        private readonly ICrudRepository<TEntity, TPrimaryKey> _repository;
        private readonly CommonMessages _commonMessages;

        protected CrudCommandHandler(
            INotificationContext notificationContext, 
            CommonMessages commonMessages,
            TUnitOfWork uow,
            ICrudRepository<TEntity, TPrimaryKey> repository) : base(commonMessages, notificationContext, uow)
        {
            _repository = repository;
            _commonMessages = commonMessages;
        }

        public virtual async Task<Unit> Handle(TDeleteCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.FindByIdAsync(request.Id);
            if (entity == null)
                return Fail(_commonMessages.NotFound ?? "Not found");

            await _repository.DeleteAsync(entity);
            await Commit();

            return Finish();
        }
    }

    public abstract class CrudCommandHandler<TEntity, TPrimaryKey, TUpdateCommand, TDeleteCommand, TUnitOfWork> :
        CrudCommandHandler<TEntity, TPrimaryKey, TDeleteCommand, TUnitOfWork>,
        ICommandHandler<TUpdateCommand, TEntity>
        where TEntity : class, IEntity<TPrimaryKey>
        where TUpdateCommand : ICommandMappedWithId<TEntity, TPrimaryKey, TEntity>
        where TDeleteCommand : ICommandWithId<TPrimaryKey>
        where TUnitOfWork : IUnitOfWork
    {
        private readonly IMapper _mapper;
        private readonly ICrudRepository<TEntity, TPrimaryKey> _repository;
        private readonly CommonMessages _commonMessages;

        protected CrudCommandHandler(
            INotificationContext notificationContext,
            CommonMessages commonMessages,
            IMapper mapper,
            TUnitOfWork uow,
            ICrudRepository<TEntity, TPrimaryKey> repository) : base(notificationContext, commonMessages, uow, repository)
        {
            _mapper = mapper;
            _repository = repository;
            _commonMessages = commonMessages;
        }

        public virtual async Task<TEntity> Handle(TUpdateCommand request, CancellationToken cancellationToken)
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

    public abstract class CrudCommandHandler<TEntity, TPrimaryKey, TRegisterCommand, TUpdateCommand, TDeleteCommand, TUnitOfWork> :
        CrudCommandHandler<TEntity, TPrimaryKey, TUpdateCommand, TDeleteCommand, TUnitOfWork>,
        ICommandHandler<TRegisterCommand, TEntity>
        where TEntity : class, IEntity<TPrimaryKey>
        where TRegisterCommand : ICommandMapped<TEntity, TPrimaryKey, TEntity>
        where TUpdateCommand : ICommandMappedWithId<TEntity, TPrimaryKey, TEntity>
        where TDeleteCommand : ICommandWithId<TPrimaryKey>
        where TUnitOfWork : IUnitOfWork
    {
        private readonly IMapper _mapper;
        private readonly ICrudRepository<TEntity, TPrimaryKey> _repository;

        protected CrudCommandHandler(
            INotificationContext notificationContext,
            CommonMessages commonMessages,
            IMapper mapper,
            TUnitOfWork uow,
            ICrudRepository<TEntity, TPrimaryKey> repository) : base(notificationContext, commonMessages, mapper, uow, repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public virtual async Task<TEntity> Handle(TRegisterCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<TEntity>(request);

            if (!IsValid<TEntity, TPrimaryKey>(entity))
                return Fail<TEntity>(null);

            await _repository.AddAsync(entity);
            await Commit();

            return entity;
        }
    }

    public abstract class CrudCommandHandlerBase<TEntity, TRegisterCommand, TUpdateCommand, TDeleteCommand, TUnitOfWork> :
        CrudCommandHandler<TEntity, int, TRegisterCommand, TUpdateCommand, TDeleteCommand, TUnitOfWork>
        where TEntity : class, IEntity
        where TRegisterCommand : ICommandMapped<TEntity, int, TEntity>
        where TUpdateCommand : ICommandMappedWithId<TEntity, int, TEntity>
        where TDeleteCommand : ICommandWithId
        where TUnitOfWork : IUnitOfWork
    {
        protected CrudCommandHandlerBase(
            INotificationContext notificationContext,
            CommonMessages commonMessages,
            IMapper mapper,
            TUnitOfWork uow,
            ICrudRepository<TEntity, int> repository) : base(notificationContext, commonMessages, mapper, uow, repository)
        {
        }
    }
}