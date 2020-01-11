using System;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Email
{
    public interface IEmail
    {
        Func<string> MessageTemplateAction { get; set; }
        string From { get; set; }
        string To { get; set; }
        string Subject { get; set; }
        bool IsHtml { get; set; }
    }
}