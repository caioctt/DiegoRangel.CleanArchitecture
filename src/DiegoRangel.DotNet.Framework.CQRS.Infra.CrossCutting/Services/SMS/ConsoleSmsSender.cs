using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.SMS
{
    public class ConsoleSmsSender : ISmsSender
    {
        private readonly ILogger _logger;
        public ConsoleSmsSender(ILogger<ConsoleSmsSender> logger)
        {
            _logger = logger;
        }

        public Task SendAsync(string to, string body)
        {
            _logger.LogInformation($"SMS sent to {to}: {body}");
            return Task.FromResult(true);
        }
    }
}