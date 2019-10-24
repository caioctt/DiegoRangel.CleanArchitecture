using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Email;
using Microsoft.Extensions.Logging;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Services.Email
{
    public class ConsoleEmailSenderService : IEmailSenderService
    {
        private readonly ILogger _logger;

        public ConsoleEmailSenderService(ILogger<ConsoleEmailSenderService> logger)
        {
            _logger = logger;
        }

        public Task Send(IEmail email, string host, short port)
        {
            _logger.LogInformation(null, $"E-mail successfully sent to: {email.To}.");
            return Task.CompletedTask;
        }
    }
}