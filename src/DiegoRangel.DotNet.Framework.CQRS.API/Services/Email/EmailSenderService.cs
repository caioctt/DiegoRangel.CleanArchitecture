using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Email;
using Microsoft.Extensions.Logging;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Services.Email
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly EmailSettings _emailSettings;
        private readonly ILogger<EmailSenderService> _logger;

        public EmailSenderService(ILogger<EmailSenderService> logger, EmailSettings emailSettings)
        {
            _logger = logger;
            _emailSettings = emailSettings;
        }

        public async Task<bool> Send(IEmail email)
        {
            try
            {
                using (var smtpClient = BuildSmtpClient())
                {
                    var mail = new MailMessage(
                        email.From ?? _emailSettings.NoReplyMail, 
                        email.To, 
                        email.Subject, 
                        email.MessageTemplateAction())
                    {
                        IsBodyHtml = email.IsHtml
                    };

                    await smtpClient.SendMailAsync(mail);
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Fail sending e-mail to {email.To}.");
                return false;
            }
        }

        private SmtpClient BuildSmtpClient()
        {
            var smtpClient = new SmtpClient(_emailSettings.Host, _emailSettings.Port)
            {
                EnableSsl = _emailSettings.EnableSsl
            };

            if (_emailSettings.UseDefaultCredentials)
            {
                smtpClient.UseDefaultCredentials = true;
            }
            else
            {
                smtpClient.UseDefaultCredentials = false;

                if (!string.IsNullOrEmpty(_emailSettings.UserName))
                    smtpClient.Credentials = new NetworkCredential(_emailSettings.UserName, _emailSettings.Password);
            }

            return smtpClient;
        }
    }
}