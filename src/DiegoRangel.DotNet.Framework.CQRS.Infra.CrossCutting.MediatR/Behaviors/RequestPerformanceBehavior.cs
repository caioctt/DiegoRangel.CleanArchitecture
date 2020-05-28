using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Session;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.MediatR.Behaviors
{
    public class RequestPerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger _logger;
        private readonly ILoggedInUserIdentifierProvider _loggedInUserProvider;

        public RequestPerformanceBehavior(ILogger<TRequest> logger, ILoggedInUserIdentifierProvider loggedInUserProvider)
        {
            _timer = new Stopwatch();
            _logger = logger;
            _loggedInUserProvider = loggedInUserProvider;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();
            var response = await next();
            _timer.Stop();

            var elapsedMilliseconds = _timer.ElapsedMilliseconds;
            if (elapsedMilliseconds <= 500) return response;

            var user = await _loggedInUserProvider.GetUserIdentifierAsync();

            var model = JsonConvert.SerializeObject(request, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc
            });

            _logger.LogWarning($"Long Running Request: [{typeof(TRequest).Name}] ({elapsedMilliseconds} milliseconds) for user [{user ?? "Anonymous"}] with model: {model};");

            return response;
        }
    }
}