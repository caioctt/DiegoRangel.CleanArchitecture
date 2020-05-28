using System;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Mailing
{
    public class EmailData
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Template { get; set; }
        public object Model { get; set; }
        public string Body { get; set; }
        public Func<string> BodyDelegate { get; set; }

        public EmailData()
        {

        }
        public EmailData(string to, string subject)
        {
            To = to;
            Subject = subject;
        }
        public EmailData(string to, string subject, string body) : this(to, subject)
        {
            Body = body;
        }
        public EmailData(string to, string subject, Func<string> bodyDelegate) : this(to, subject)
        {
            BodyDelegate = bodyDelegate;
        }
        public EmailData(string to, string subject, string template, object model) : this(to, subject)
        {
            Template = template;
            Model = model;
        }

        public EmailData WithAddress(string emailAddress)
        {
            To = emailAddress;
            return this;
        }
        public EmailData WithSubject(string subject)
        {
            Subject = subject;
            return this;
        }
        public EmailData WithBody(string body)
        {
            Body = body;
            return this;
        }
        public EmailData WithBody(Func<string> bodyDelegate)
        {
            BodyDelegate = bodyDelegate;
            return this;
        }
        public EmailData UsingTemplate(string template, object model)
        {
            Template = template;
            Model = model;
            return this;
        }
    }
}