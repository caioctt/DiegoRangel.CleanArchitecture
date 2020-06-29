using System.Collections.Generic;
using FluentValidation.Results;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.NotificationPattern
{
    public interface INotificationContext
    {
        IReadOnlyCollection<Notification> Notifications { get; }
        bool HasNotifications { get; }
        void AddNotification(string message);
        void AddNotifications(string[] messages);
        void AddNotification(Notification notification);
        void AddNotifications(IReadOnlyCollection<Notification> notifications);
        void AddNotifications(IList<Notification> notifications);
        void AddNotifications(ICollection<Notification> notifications);
        void AddNotifications(ValidationResult validationResult);
    }
}