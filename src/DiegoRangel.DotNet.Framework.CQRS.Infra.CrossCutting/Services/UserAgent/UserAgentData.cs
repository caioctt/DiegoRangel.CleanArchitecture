namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.UserAgent
{
    public class UserAgentData : IUserAgentData
    {
        public string RemoteIpAddress { get; set; }
        public string OperatingSystem { get; set; }
        public string UserAgentFamily { get; set; }
        public string UserAgentVersion { get; set; }
    }
}