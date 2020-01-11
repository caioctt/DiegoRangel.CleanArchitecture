using System;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Commands;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Analytics.Commands
{
    public class TrackResponseCommand : ICommandMapped<ResponseTracking, Guid>
    {
        public string Endpoint { get; set; }
        public string Protocol { get; set; }
        public string Method { get; set; }
        public string Platform { get; set; }
        public string Browser { get; set; }
        public string Mobile { get; set; }
        public string Robot { get; set; }
        public bool IsMobile { get; set; }
        public bool IsBrowser { get; set; }
        public bool IsRobot { get; set; }
        public long ResponseTimeInMilliseconds { get; set; }
        public short StatusCode { get; set; }
        public DateTime DateTime { get; set; }

        public TrackResponseCommand(string endpoint, string protocol, string method, string platform, string browser, string mobile, string robot, bool isMobile, bool isBrowser, bool isRobot, long responseTimeInMilliseconds, short statusCode)
        {
            Endpoint = endpoint;
            Protocol = protocol;
            Method = method;
            Platform = platform;
            Browser = browser;
            Mobile = mobile;
            Robot = robot;
            IsMobile = isMobile;
            IsBrowser = isBrowser;
            IsRobot = isRobot;
            ResponseTimeInMilliseconds = responseTimeInMilliseconds;
            StatusCode = statusCode;
            DateTime = DateTime.Now;
        }
    }
}