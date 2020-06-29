using System.Linq;
using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Interfaces;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Messages;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.NotificationPattern;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Handlers
{
    public abstract class CommandHandlerBase<TUnitOfWork>
        where TUnitOfWork : IUnitOfWork
    {
        private readonly TUnitOfWork _uow;
        private readonly INotificationContext _notificationContext;
        private readonly CommonMessages _commonMessages;

        protected CommandHandlerBase(
            INotificationContext notificationContext,
            CommonMessages commonMessages,
            TUnitOfWork uow)
        {
            _uow = uow;
            _commonMessages = commonMessages;
            _notificationContext = notificationContext;
        }

        protected Unit Finish() => Unit.Value;
        protected TResponse Fail<TResponse>(TResponse response)
        {
            return response;
        }
        protected Unit Fail(string message)
        {
            _notificationContext.AddNotification(message);
            return Unit.Value;
        }
        protected TResponse Fail<TResponse>(string message, TResponse response)
        {
            _notificationContext.AddNotification(message);
            return response;
        }
        protected Unit Fail(string[] messages)
        {
            _notificationContext.AddNotifications(messages);
            return Unit.Value;
        }
        protected TResponse Fail<TResponse>(string[] messages, TResponse response)
        {
            _notificationContext.AddNotifications(messages);
            return response;
        }
        protected Unit Fail(IdentityResult result)
        {
            return Fail(result, Unit.Value);
        }
        protected TResponse Fail<TResponse>(IdentityResult result, TResponse response)
        {
            _notificationContext.AddNotifications(result.Errors?.Select(x => x.Code).ToArray());
            return response;
        }

        protected bool IsValid<T, TPrimaryKey>(T entity)
            where T : IEntity<TPrimaryKey>
        {
            if (entity.IsValid()) return true;
            _notificationContext.AddNotifications(entity.ValidationResult);
            return false;
        }
        protected bool IsValid<T>(T entity) where T : IEntity<int>
        {
            return IsValid<T, int>(entity);
        }

        protected void AddNotification(string message) => _notificationContext.AddNotification(message);

        protected async Task<bool> Commit()
        {
            if (_notificationContext.HasNotifications) return false;
            if (await _uow.CommitAsync())
                return true;

            _notificationContext.AddNotification(_commonMessages.UnhandledOperation ?? "Oops... We could't perform this operation at this time.");
            return false;
        }
    }
}