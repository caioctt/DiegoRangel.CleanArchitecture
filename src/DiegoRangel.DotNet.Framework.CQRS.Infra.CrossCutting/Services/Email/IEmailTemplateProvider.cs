namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Email
{
    public interface IEmailTemplateProvider
    {
        string Build(string title, string body);
    }
}