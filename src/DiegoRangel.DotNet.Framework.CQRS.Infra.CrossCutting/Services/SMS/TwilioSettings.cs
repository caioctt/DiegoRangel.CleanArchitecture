namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.SMS
{
    public class TwilioSettings
    {
        public string FromNumber { get; set; }
        public string Sid { get; set; }
        public string Token { get; set; }
    }
}