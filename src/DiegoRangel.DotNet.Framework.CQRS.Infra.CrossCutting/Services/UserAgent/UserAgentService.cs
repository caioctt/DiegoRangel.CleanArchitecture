using Microsoft.AspNetCore.Http;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.UserAgent
{
    public class UserAgentService : IUserAgentService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserAgentService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IUserAgentData GetCurrentUserAgentData()
        {
            var userAgentData = new UserAgentData();
            var remoteIpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress;
            if (remoteIpAddress != null)
                userAgentData.RemoteIpAddress = remoteIpAddress.ToString();

            var userAgent = _httpContextAccessor.HttpContext.Request.Headers["User-Agent"];
            if (string.IsNullOrEmpty(userAgent)) return userAgentData;

            var uaParser = UAParser.Parser.GetDefault();
            var clientInfo = uaParser.Parse(userAgent);
            userAgentData.OperatingSystem = clientInfo.OS.ToString();
            userAgentData.UserAgentFamily = clientInfo.UA.Family;
            userAgentData.UserAgentVersion = $"{clientInfo.UA.Major}.{clientInfo.UA.Minor}.{clientInfo.UA.Patch}";

            return userAgentData;
        }
    }
}