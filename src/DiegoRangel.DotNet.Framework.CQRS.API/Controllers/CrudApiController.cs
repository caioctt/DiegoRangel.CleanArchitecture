using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.API.Mapper;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Commands;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Messages;
using Microsoft.AspNetCore.Mvc;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Controllers
{
    public abstract class CrudApiController<TEntity, TEntityKey, TRepository, TAddCommandRequest, TUpdateCommand, TDeleteCommand, TViewModel, TViewModelList, TViewModelInput, TViewModelUpdate> : 
        ApiControllerBase
        where TEntity : class, IEntity<TEntityKey>
        where TRepository : ICrudRepository<TEntity, TEntityKey>
        where TAddCommandRequest : ICommandMapped<TEntity, TEntityKey, TEntity>
        where TUpdateCommand : ICommandMappedWithId<TEntity, TEntityKey, TEntity>
        where TDeleteCommand : ICommandWithId<TEntityKey>
        where TViewModelList : IViewModelWithId<TEntity, TEntityKey>
        where TViewModel : IViewModelWithId<TEntity, TEntityKey>
        where TViewModelInput : IViewModel<TEntity>
        where TViewModelUpdate : IViewModel<TEntity>
    {
        private readonly CommonMessages _commonMessages;
        private readonly TRepository _repository;

        protected CrudApiController(CommonMessages commonMessages, TRepository repository)
        {
            _repository = repository;
            _commonMessages = commonMessages;
        }

        [HttpGet]
        public virtual async Task<IActionResult> FindAll()
        {
            var result = await _repository.FindAllAsync();
            return Ok(Mapper.Map<IEnumerable<TViewModelList>>(result));
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> FindById(TEntityKey id)
        {
            var result = await _repository.FindByIdAsync(id);
            if (result != null) return Ok(Mapper.Map<TViewModel>(result));

            SetValidationError(_commonMessages.NotFound ?? "Not found");
            return BadRequest();
        }

        [HttpPost]
        public virtual async Task<IActionResult> Add([FromBody]TViewModelInput input)
        {
            var cmd = Mapper.Map<TAddCommandRequest>(input);
            var result = await Mediator.Send(cmd);
            return Ok(Mapper.Map<TViewModel>(result));
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Update(TEntityKey id, [FromBody]TViewModelUpdate input)
        {
            var cmd = Mapper.Map<TUpdateCommand>(input);
            cmd.Id = id;

            var result = await Mediator.Send(cmd);
            return Ok(Mapper.Map<TViewModel>(result));
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(TEntityKey id)
        {
            var cmd = Activator.CreateInstance<TDeleteCommand>();
            cmd.Id = id;

            await Mediator.Send(cmd);
            return Ok();
        }
    }

    public abstract class CrudApiController<TEntity, TRepository, TAddCommandRequest, TUpdateCommand, TDeleteCommand, TViewModel, TViewModelList, TViewModelInput, TViewModelUpdate> :
        CrudApiController<TEntity, int, TRepository, TAddCommandRequest, TUpdateCommand, TDeleteCommand, TViewModel, TViewModelList, TViewModelInput, TViewModelUpdate>
        where TEntity : class, IEntity<int>
        where TRepository : ICrudRepository<TEntity, int>
        where TAddCommandRequest : ICommandMapped<TEntity, int, TEntity>
        where TUpdateCommand : ICommandMappedWithId<TEntity, int, TEntity>
        where TDeleteCommand : ICommandWithId<int>
        where TViewModelList : IViewModelWithId<TEntity, int>
        where TViewModel : IViewModelWithId<TEntity, int>
        where TViewModelInput : IViewModel<TEntity>
        where TViewModelUpdate : IViewModel<TEntity>
    {
        protected CrudApiController(CommonMessages commonMessages, TRepository repository) : base(commonMessages, repository)
        {
        }
    }
}