using System.Threading.Tasks;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.SMS
{
    public interface ISmsSender
    {
        Task SendAsync(string to, string body);
    }
}