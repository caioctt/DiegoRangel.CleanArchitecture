using System;
using System.Threading.Tasks;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Mailing
{
    public interface IEmailSender
    {
        IEmailSender To(string emailAdrress);
        IEmailSender Subject(string subject);
        IEmailSender Body(string body);
        IEmailSender Body(Func<string> bodyDelegate);
        IEmailSender UsingTemplate(string template, object model);
        void Enqueue();
        Task<bool> SendAsync();
        Task<bool> SendAsync(EmailData emailData);
        Task SendAllAsync();
    }
}