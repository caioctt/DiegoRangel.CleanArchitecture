using System;
using System.IO;
using System.Threading.Tasks;
using FluentEmail.Core.Interfaces;
using Microsoft.Extensions.Logging;
using RazorLight.Extensions;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Mailing
{
    public class ConsoleEmailSender : IEmailSender
    {
        private readonly MailTemplatingSettings _mailSettings;
        private readonly ITemplateRenderer _templateRenderer;
        private readonly ILogger _logger;
        private readonly IEmailQueue _emailQueue;

        private EmailData _emailData;

        public ConsoleEmailSender(MailTemplatingSettings mailSettings, ILogger<ConsoleEmailSender> logger, ITemplateRenderer templateRenderer, IEmailQueue emailQueue)
        {
            _logger = logger;
            _templateRenderer = templateRenderer;
            _emailQueue = emailQueue;
            _mailSettings = mailSettings;
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
        public Task<bool> SendAsync(EmailData emailData)
        {
            var link = emailData.Model?.GetType().GetProperty("Link")?.GetValue(emailData.Model, null);
            _logger.LogInformation($"Email sent to: {emailData.To} [{emailData.Subject}] {link}");

            return Task.FromResult(true);
        }
        public async Task SendAllAsync()
        {
            if (!_emailQueue.HasEmailsToSend()) return;
            foreach (var email in _emailQueue.Emails)
            {
                await SendAsync(email);
            }
        }

        private async Task<string> ParseTemplateAsync()
        {
            if (string.IsNullOrEmpty(_emailData.Template)) return null;

            string message;
            var tplNamespace = $"{_mailSettings.EmailTemplatesNamespace}.{_emailData.Template}.cshtml";

            await using (var stream = _mailSettings.EmailTemplatesDiscoveryType.Assembly.GetManifestResourceStream(tplNamespace))
            {
                if (stream == null) return null;
                using (var reader = new StreamReader(stream))
                {
                    message = await reader.ReadToEndAsync();
                }
            }

            return await _templateRenderer.ParseAsync(message, _emailData.Model.ToExpando());
        }
        private void Clear()
        {
            _emailData = new EmailData();
        }
    }
}