using System;
using System.Reflection;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Mailing
{
    public class MailSettings
    {
        public string DefaultFromEmail { get; set; }
        public string DefaultFromName { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class MailTemplatingSettings
    {
        public string EmailTemplatesNamespace { get; set; }
        public Type EmailTemplatesDiscoveryType { get; set; }
    }
}