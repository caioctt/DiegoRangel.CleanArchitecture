using System;
using System.Net;
using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Middlewares
{
    public class RequestMiddleware
    {
        private readonly CommonMessages _commonMessages;
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public RequestMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, CommonMessages commonMessages)
        {
            _next = next;
            _commonMessages = commonMessages;
            _logger = loggerFactory.CreateLogger<RequestMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = (int)HttpStatusCode.InternalServerError;
            var result = JsonConvert.SerializeObject(new
            {
                success = false,
                errors = new[] { _commonMessages.UnhandledOperation ?? "Oops... We encountered a failure while attempting this operation at this time." }
            });

            _logger.LogError(exception, "Unhandled exception detected on RequestMiddleware.", null);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = code;

            return context.Response.WriteAsync(result);
        }
    }
}