namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Notifications
{
    public class DomainNotification
    {
        public string Message { get; }

        public DomainNotification(string message)
        {
            Message = message;
        }
    }
}