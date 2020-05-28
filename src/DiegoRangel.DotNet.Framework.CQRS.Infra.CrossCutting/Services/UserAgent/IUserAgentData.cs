namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.UserAgent
{
    public interface IUserAgentData
    {
        string RemoteIpAddress { get; set; }
        string OperatingSystem { get; set; }
        string UserAgentFamily { get; set; }
        string UserAgentVersion { get; set; }
    }
}