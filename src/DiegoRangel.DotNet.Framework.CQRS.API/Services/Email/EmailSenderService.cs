using System.Net.Mail;
using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Email;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Services.Email
{
    public class EmailSenderService : IEmailSenderService
    {
        public async Task Send(IEmail email, string host, short port)
        {
            var smtpClient = new SmtpClient(host, port);
            var message = new MailMessage
            {
                From = new MailAddress(email.From),
                Subject = email.Subject,
                Body = email.Body,
                IsBodyHtml = true
            };

            if (string.IsNullOrEmpty(email.To)) return;

            var destinations = email.To.Split(";");

            if (destinations?.Length > 0)
            {
                foreach (var x in destinations)
                    message.To.Add(new MailAddress(x.ToLower().Trim()));
            }

            await smtpClient.SendMailAsync(message);
        }
    }
}