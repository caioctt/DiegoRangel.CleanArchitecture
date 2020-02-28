using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Interfaces;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Notifications;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Responses;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.MediatR;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Messages;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Handlers
{
    public abstract class CommandHandlerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly DomainNotificationContext _notificationContext;
        private readonly CommonMessages _commonMessages;

        protected IResponse Ok(object obj = null) => new Ok(obj);
        protected IResponse NoContent() => new NoContent();
        protected IResponse Fail() => new Fail();
        protected IResponse Fail(string message)
        {
            _notificationContext.AddNotification(message);
            return new Fail();
        }
        protected IResponse Fail(string[] messages)
        {
            _notificationContext.AddNotifications(messages);
            return new Fail();
        }

        protected CommandHandlerBase(
            DomainNotificationContext notificationContext, 
            CommonMessages commonMessages,
            IUnitOfWork uow)
        {
            _uow = uow;
            _commonMessages = commonMessages;
            _notificationContext = notificationContext;
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
            if (await _uow.Commit())
                return true;

            _notificationContext.AddNotification(_commonMessages.UnhandledOperation ?? "Oops... We could't perform this operation at this time.");
            return false;
        }
    }
}