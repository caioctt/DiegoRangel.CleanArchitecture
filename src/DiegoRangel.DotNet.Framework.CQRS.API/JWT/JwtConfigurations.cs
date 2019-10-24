namespace DiegoRangel.DotNet.Framework.CQRS.API.JWT
{
    public class JwtConfigurations
    {
        public string Key { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int MinutesToExpire { get; set; }
    }
}