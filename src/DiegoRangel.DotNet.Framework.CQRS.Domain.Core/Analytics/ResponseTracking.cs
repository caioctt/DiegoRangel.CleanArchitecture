using System;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Analytics
{
    public class ResponseTracking : Entity<Guid>
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
    }
}