using System;
using System.Collections.Generic;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Email
{
    public class EmailQueue : IEmailQueue
    {
        private readonly IEmailSenderService _emailSenderService;

        public EmailQueue(IEmailSenderService emailSenderService)
        {
            _emailSenderService = emailSenderService;
        }

        public IList<IEmail> Emails { get; set; }

        public void Add(Func<string> emailTemplateAction, string subject, string to, string from = null)
        {
            if (Emails == null) Emails = new List<IEmail>();

            Emails.Add(new Email
            {
                MessageTemplateAction = emailTemplateAction,
                Subject = subject,
                From = from,
                To = to,
                IsHtml = true
            });
        }

        public void SendAll()
        {
            if (Emails == null || Emails.Count == 0) return;

            foreach (var email in Emails)
                _emailSenderService.Send(email);

            Emails = null;
        }
    }
}