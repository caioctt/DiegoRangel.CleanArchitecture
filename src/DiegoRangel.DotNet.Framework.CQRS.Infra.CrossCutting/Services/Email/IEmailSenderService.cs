using System.Threading.Tasks;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Email
{
    public interface IEmailSenderService
    {
        Task<bool> Send(IEmail email);
    }
}