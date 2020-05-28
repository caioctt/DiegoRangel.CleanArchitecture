namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.NotificationPattern
{
    public class Notification
    {
        public string Message { get; }

        public Notification(string message)
        {
            Message = message;
        }
    }
}