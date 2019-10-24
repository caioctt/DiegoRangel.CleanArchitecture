namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Email
{
    public interface IEmailTemplateProvider
    {
        string Build(string appName, string title, string body, string copyright, string linkButtonLabel, string link);
    }
}