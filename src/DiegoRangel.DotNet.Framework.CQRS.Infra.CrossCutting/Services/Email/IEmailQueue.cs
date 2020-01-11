using System;
using System.Collections.Generic;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Email
{
    public interface IEmailQueue
    {
        IList<IEmail> Emails { get; set; }
        void Add(Func<string> emailTemplateAction, string subject, string to, string from = null);
        void SendAll();
    }
}