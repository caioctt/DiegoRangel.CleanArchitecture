using System.Linq;
using AutoMapper;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Notifications;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Responses;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Controllers
{
    public abstract class ApiControllerBase : Controller
    {
        private readonly IMapper _mapper;
        private readonly DomainNotificationContext _domainNotificationContext;
        protected ApiControllerBase(DomainNotificationContext domainNotificationContext, IMapper mapper)
        {
            _domainNotificationContext = domainNotificationContext;
            _mapper = mapper;
        }

        protected new IActionResult Response(object data = null)
        {
            if (!_domainNotificationContext.HasNotifications)
                return base.Ok(FormatSuccessResponse(data));

            return base.BadRequest(FormatFailResponse(_domainNotificationContext.Notifications.Select(x => x.Message).ToArray()));
        }
        protected new IActionResult Response(IResponse response)
        {
            switch (response)
            {
                case Ok ok:
                    return base.Ok(FormatSuccessResponse(ok.Result));
                case Fail _:
                    return base.BadRequest(FormatFailResponse(_domainNotificationContext.Notifications.Select(x => x.Message).ToArray()));
                default:
                    return base.Ok(FormatSuccessResponse(null));
            }
        }
        protected new IActionResult Response<T>(IResponse response)
        {
            switch (response)
            {
                case Ok ok:
                    return base.Ok(FormatSuccessResponse<T>(ok.Result));
                case Fail _:
                    return base.BadRequest(FormatFailResponse(_domainNotificationContext.Notifications.Select(x => x.Message).ToArray()));
                default:
                    return base.Ok(FormatSuccessResponse<T>(null));
            }
        }

        protected new IActionResult Ok(object data = null)
        {
            return base.Ok(FormatSuccessResponse(data));
        }
        protected IActionResult Ok<T>(object data = null)
        {
            return base.Ok(FormatSuccessResponse<T>(data));
        }
        protected new IActionResult BadRequest()
        {
            return base.BadRequest(FormatFailResponse(_domainNotificationContext.Notifications.Select(x => x.Message).ToArray()));
        }

        private static object FormatSuccessResponse(object data)
        {
            return new
            {
                success = true,
                data = data
            };
        }
        private object FormatSuccessResponse<T>(object data)
        {
            return new
            {
                success = true,
                data = _mapper.Map<T>(data)
            };
        }
        private static object FormatFailResponse(string[] errors)
        {
            return new
            {
                success = false,
                errors = errors
            };
        }

        protected bool IsViewModelInvalid()
        {
            if (ModelState.IsValid) return false;

            var erros = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotifyDomainError(erroMsg);
            }

            return true;
        }

        protected void NotifyDomainError(string mensagem)
        {
            _domainNotificationContext.AddNotification(mensagem);
        }
    }
}