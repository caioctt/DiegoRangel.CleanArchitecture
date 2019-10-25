using AutoMapper;
using DiegoRangel.DotNet.Framework.CQRS.API.Mapper;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Commands;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Notifications;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories;
using MediatR;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Controllers
{
    public abstract class CrudSoftDeletableApiController<TEntity, TEntityKey, TRepository, TAddCommandRequest, TUpdateCommand, TDeleteCommand, TViewModel, TViewModelList, TViewModelInput, TViewModelUpdate> :
        CrudApiController<TEntity, TEntityKey, TRepository, TAddCommandRequest, TUpdateCommand, TDeleteCommand, TViewModel, TViewModelList, TViewModelInput, TViewModelUpdate>
        where TEntityKey : struct
        where TEntity : class, IEntity<TEntityKey>, ISoftDelete
        where TRepository : ICrudSoftDeletableRepository<TEntity, TEntityKey>
        where TAddCommandRequest : ICommandMapped<TEntity, TEntityKey>
        where TUpdateCommand : ICommandMappedWithId<TEntity, TEntityKey>
        where TDeleteCommand : ICommandWithId<TEntityKey>
        where TViewModelList : IViewModelWithId<TEntity, TEntityKey>
        where TViewModel : IViewModelWithId<TEntity, TEntityKey>
        where TViewModelInput : IViewModel<TEntity>
        where TViewModelUpdate : IViewModel<TEntity>
    {
        protected CrudSoftDeletableApiController(DomainNotificationContext domainNotificationContext, IMapper mapper, IMediator mediator, TRepository repository) : base(domainNotificationContext, mapper, mediator, repository)
        {
        }
    }

    public abstract class CrudSoftDeletableApiController<TEntity, TRepository, TAddCommandRequest, TUpdateCommand, TDeleteCommand, TViewModel, TViewModelList, TViewModelInput, TViewModelUpdate> :
        CrudSoftDeletableApiController<TEntity, int, TRepository, TAddCommandRequest, TUpdateCommand, TDeleteCommand, TViewModel, TViewModelList, TViewModelInput, TViewModelUpdate>
        where TEntity : class, IEntity<int>, ISoftDelete
        where TRepository : ICrudSoftDeletableRepository<TEntity, int>
        where TAddCommandRequest : ICommandMapped<TEntity, int>
        where TUpdateCommand : ICommandMappedWithId<TEntity, int>
        where TDeleteCommand : ICommandWithId<int>
        where TViewModelList : IViewModelWithId<TEntity, int>
        where TViewModel : IViewModelWithId<TEntity, int>
        where TViewModelInput : IViewModel<TEntity>
        where TViewModelUpdate : IViewModel<TEntity>
    {
        protected CrudSoftDeletableApiController(DomainNotificationContext domainNotificationContext, IMapper mapper, IMediator mediator, TRepository repository) : base(domainNotificationContext, mapper, mediator, repository)
        {
        }
    }
}