using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Interfaces;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Notifications;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Responses;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Handlers
{
    public abstract class CommandHandlerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly DomainNotificationContext _notificationContext;

        protected IResponse Ok(object obj = null) => new Ok(obj);
        protected IResponse NoContent() => new NoContent();
        protected IResponse Fail() => new Fail();

        protected CommandHandlerBase(
            DomainNotificationContext notificationContext,
            IUnitOfWork uow)
        {
            _uow = uow;
            _notificationContext = notificationContext;
        }

        protected bool IsValid<T, TPrimaryKey>(T entity)
            where T : IEntity<TPrimaryKey>
            where TPrimaryKey : struct
        {
            if (entity.IsValid()) return true;
            _notificationContext.AddNotifications(entity.ValidationResult);
            return false;
        }
        protected bool IsValid<T>(T entity) where T : IEntity<long>
        {
            return IsValid<T, long>(entity);
        }

        protected void AddNotification(string message) => _notificationContext.AddNotification(message);

        protected async Task<bool> Commit()
        {
            if (_notificationContext.HasNotifications) return false;
            if (await _uow.Commit())
                return true;

            return false;
        }
    }
}