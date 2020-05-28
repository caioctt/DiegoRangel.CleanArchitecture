using System.Collections.Generic;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Mailing
{
    public interface IEmailQueue
    {
        IList<EmailData> Emails { get; set; }
        void Add(EmailData emailData);
        void Clear();
        bool HasEmailsToSend();
    }
}