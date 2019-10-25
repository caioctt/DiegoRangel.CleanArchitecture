using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DiegoRangel.DotNet.Framework.CQRS.API.Mapper;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Commands;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Notifications;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Messages;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Controllers
{
    public abstract class CrudApiController<TEntity, TEntityKey, TRepository, TAddCommandRequest, TUpdateCommand, TDeleteCommand, TViewModel, TViewModelList, TViewModelInput, TViewModelUpdate> : 
        ApiControllerBase
        where TEntityKey : struct
        where TEntity : class, IEntity<TEntityKey>
        where TRepository : ICrudRepository<TEntity, TEntityKey>
        where TAddCommandRequest : ICommandMapped<TEntity, TEntityKey>
        where TUpdateCommand : ICommandMappedWithId<TEntity, TEntityKey>
        where TDeleteCommand : ICommandWithId<TEntityKey>
        where TViewModelList : IViewModelWithId<TEntity, TEntityKey>
        where TViewModel : IViewModelWithId<TEntity, TEntityKey>
        where TViewModelInput : IViewModel<TEntity>
        where TViewModelUpdate : IViewModel<TEntity>
    {
        private readonly CommonMessages _commonMessages;
        private readonly TRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        protected CrudApiController(DomainNotificationContext domainNotificationContext, CommonMessages commonMessages, IMapper mapper, IMediator mediator, TRepository repository) : base(domainNotificationContext, mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
            _repository = repository;
            _commonMessages = commonMessages;
        }

        [HttpGet]
        public virtual async Task<IActionResult> FindAll()
        {
            var result = await _repository.FindAllAsync();
            return Ok(_mapper.Map<IEnumerable<TViewModelList>>(result));
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> FindById(TEntityKey id)
        {
            var result = await _repository.FindByIdAsync(id);
            if (result != null) return Ok(_mapper.Map<TViewModel>(result));

            NotifyDomainError(_commonMessages.NotFound ?? "Not found");
            return BadRequest();
        }

        [HttpPost]
        public virtual async Task<IActionResult> Add([FromBody]TViewModelInput input)
        {
            var cmd = _mapper.Map<TAddCommandRequest>(input);
            var result = await _mediator.Send(cmd);
            return Response<TViewModel>(result);
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Update(TEntityKey id, [FromBody]TViewModelUpdate input)
        {
            var cmd = _mapper.Map<TUpdateCommand>(input);
            cmd.Id = id;

            var result = await _mediator.Send(cmd);
            return Response<TViewModel>(result);
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(TEntityKey id)
        {
            var cmd = Activator.CreateInstance<TDeleteCommand>();
            cmd.Id = id;

            var result = await _mediator.Send(cmd);
            return Response(result);
        }
    }

    public abstract class CrudApiController<TEntity, TRepository, TAddCommandRequest, TUpdateCommand, TDeleteCommand, TViewModel, TViewModelList, TViewModelInput, TViewModelUpdate> :
        CrudApiController<TEntity, int, TRepository, TAddCommandRequest, TUpdateCommand, TDeleteCommand, TViewModel, TViewModelList, TViewModelInput, TViewModelUpdate>
        where TEntity : class, IEntity<int>
        where TRepository : ICrudRepository<TEntity, int>
        where TAddCommandRequest : ICommandMapped<TEntity, int>
        where TUpdateCommand : ICommandMappedWithId<TEntity, int>
        where TDeleteCommand : ICommandWithId<int>
        where TViewModelList : IViewModelWithId<TEntity, int>
        where TViewModel : IViewModelWithId<TEntity, int>
        where TViewModelInput : IViewModel<TEntity>
        where TViewModelUpdate : IViewModel<TEntity>
    {
        protected CrudApiController(DomainNotificationContext domainNotificationContext, CommonMessages commonMessages, IMapper mapper, IMediator mediator, TRepository repository) : base(domainNotificationContext, commonMessages, mapper, mediator, repository)
        {
        }
    }
}