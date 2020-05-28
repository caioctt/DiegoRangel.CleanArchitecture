using System.Collections.Generic;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Mailing
{
    public class EmailQueue : IEmailQueue
    {
        public IList<EmailData> Emails { get; set; }

        public void Add(EmailData emailData)
        {
            Emails ??= new List<EmailData>();
            Emails.Add(emailData);
        }
        public void Clear()
        {
            Emails = null;
        }
        public bool HasEmailsToSend()
        {
            return Emails?.Count > 0;
        }
    }
}