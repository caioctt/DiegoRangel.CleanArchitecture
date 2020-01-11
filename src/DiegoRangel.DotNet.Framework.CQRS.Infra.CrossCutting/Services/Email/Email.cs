using System;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Email
{
    public class Email : IEmail
    {
        public Func<string> MessageTemplateAction { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public bool IsHtml { get; set; }
    }
}