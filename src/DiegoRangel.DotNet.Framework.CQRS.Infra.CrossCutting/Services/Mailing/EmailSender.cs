using System;
using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.NotificationPattern;
using FluentEmail.Core;
using RazorLight.Extensions;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Mailing
{
    public class EmailSender : IEmailSender
    {
        private readonly INotificationContext _notificationContext;
        private readonly MailTemplatingSettings _mailSettings;
        private readonly IFluentEmail _fluentEmail;
        private readonly IEmailQueue _emailQueue;

        private EmailData _emailData;

        public EmailSender(INotificationContext notificationContext, MailTemplatingSettings mailSettings, IFluentEmail fluentEmail, IEmailQueue emailQueue)
        {
            _notificationContext = notificationContext;
            _fluentEmail = fluentEmail;
            _emailQueue = emailQueue;
            _mailSettings = mailSettings;
        }

        private void Clear()
        {
            _emailData = new EmailData();
        }

        public IEmailSender To(string emailAdrress)
        {
            Clear();
            _emailData.WithAddress(emailAdrress);
            return this;
        }
        public IEmailSender Subject(string subject)
        {
            _emailData.WithSubject(subject);
            return this;
        }
        public IEmailSender Body(string body)
        {
            _emailData.WithBody(body);
            return this;
        }
        public IEmailSender Body(Func<string> bodyDelegate)
        {
            _emailData.WithBody(bodyDelegate);
            return this;
        }
        public IEmailSender UsingTemplate(string template, object model)
        {
            _emailData.UsingTemplate(template, model);
            return this;
        }

        public void Enqueue()
        {
            _emailQueue.Add(_emailData);
            Clear();
        }

        public Task<bool> SendAsync()
        {
            return SendAsync(_emailData);
        }
        public async Task<bool> SendAsync(EmailData emailData)
        {
            var sender = _fluentEmail
                .To(emailData.To)
                .Subject(emailData.Subject);

            if (!string.IsNullOrEmpty(emailData.Template))
            {
                var tplNamespace = $"{_mailSettings.EmailTemplatesNamespace}.{emailData.Template}.cshtml";
                sender.UsingTemplateFromEmbedded(tplNamespace, emailData.Model.ToExpando(), _mailSettings.EmailTemplatesDiscoveryType.Assembly);
            }
            else if (emailData.BodyDelegate != null)
                sender.Body(emailData.BodyDelegate());
            else
                sender.Body(emailData.Body);

            var result = await sender.SendAsync();
            if (result.Successful) return true;
            _notificationContext.AddNotifications(result.ErrorMessages.ToArray());
            return false;
        }
        public async Task SendAllAsync()
        {
            if (!_emailQueue.HasEmailsToSend()) return;
            foreach (var email in _emailQueue.Emails)
            {
                await SendAsync(email);
            }
        }
    }
}