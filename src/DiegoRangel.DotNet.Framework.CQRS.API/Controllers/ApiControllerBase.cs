using System.Linq;
using AutoMapper;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.NotificationPattern;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Controllers
{
    public abstract class ApiControllerBase : Controller
    {
        protected INotificationContext NotificationContext => _domainNotificationContext ??= HttpContext.RequestServices.GetService<INotificationContext>();
        private INotificationContext _domainNotificationContext;

        protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetService<IMapper>();
        private IMapper _mapper;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        private IMediator _mediator;

        protected bool IsViewModelInvalid()
        {
            if (ModelState.IsValid) return false;

            var erros = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                SetValidationError(erroMsg);
            }

            return true;
        }
        protected void SetValidationError(string mensagem)
        {
            _domainNotificationContext.AddNotification(mensagem);
        }
    }
}