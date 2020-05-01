using System.Threading;
using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Session;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Behaviours
{
    public class RequestLoggerBehavior<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly ILoggedInUserIdentifierProvider _loggedInUserProvider;

        public RequestLoggerBehavior(ILoggedInUserIdentifierProvider loggedInUserProvider, ILogger<TRequest> logger)
        {
            _loggedInUserProvider = loggedInUserProvider;
            _logger = logger;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var user = await _loggedInUserProvider.GetUserIdentifierAsync();
            _logger.LogInformation($"Proccessing Request: [{typeof(TRequest).Name}] for user [{user ?? "System"}] with model: {JsonConvert.SerializeObject(request)};");
        }
    }
}