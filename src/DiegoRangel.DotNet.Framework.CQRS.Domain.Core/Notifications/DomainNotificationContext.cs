using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Notifications
{
    public class DomainNotificationContext
    {
        private readonly List<DomainNotification> _notifications;
        public IReadOnlyCollection<DomainNotification> Notifications => _notifications;
        public bool HasNotifications => _notifications.Any();

        public DomainNotificationContext()
        {
            _notifications = new List<DomainNotification>();
        }

        public void AddNotification(string message)
        {
            _notifications.Add(new DomainNotification(message));
        }
        public void AddNotification(DomainNotification notification)
        {
            _notifications.Add(notification);
        }
        public void AddNotifications(IReadOnlyCollection<DomainNotification> notifications)
        {
            _notifications.AddRange(notifications);
        }
        public void AddNotifications(IList<DomainNotification> notifications)
        {
            _notifications.AddRange(notifications);
        }
        public void AddNotifications(ICollection<DomainNotification> notifications)
        {
            _notifications.AddRange(notifications);
        }
        public void AddNotifications(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                AddNotification(error.ErrorMessage);
            }
        }
    }
}