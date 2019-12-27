namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Settings
{
    public class MongoSettings
    {
        public string Connection { get; set; }
        public string ApplicationDatabase { get; set; }
        public string HangfireDatabase { get; set; }
    }
}