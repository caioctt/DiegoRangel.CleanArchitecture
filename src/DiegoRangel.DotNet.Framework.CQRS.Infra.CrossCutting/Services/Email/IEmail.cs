namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Email
{
    public interface IEmail
    {
        string From { get; set; }
        string To { get; set; }
        string Subject { get; set; }
        string Body { get; set; }
    }
}