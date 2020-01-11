using System;
using System.Diagnostics;
using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Analytics.Commands;
using Microsoft.AspNetCore.Http;
using UserAgentParser;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Middlewares
{
    public class ResponseTimeMiddleware
    {
        private const string RESPONSE_HEADER_RESPONSE_TIME = "X-Response-Time-ms";
        private readonly RequestDelegate _next;
        private readonly Action<TrackResponseCommand> _callbackAction;

        public ResponseTimeMiddleware(RequestDelegate next, Action<TrackResponseCommand> callbackAction)
        {
            _next = next;
            _callbackAction = callbackAction;
        }

        public Task InvokeAsync(HttpContext context)
        {
            var watch = new Stopwatch();
            watch.Start();
            context.Response.OnStarting(() => {
                watch.Stop();
                context.Response.Headers[RESPONSE_HEADER_RESPONSE_TIME] = watch.ElapsedMilliseconds.ToString();

                var userAgent = UserAgent.Parse(context.Request.Headers["User-Agent"].ToString());

                _callbackAction(new TrackResponseCommand(
                    context.Request.Path, 
                    context.Request.Scheme,
                    context.Request.Method, 
                    userAgent.Platform, 
                    userAgent.Browser, 
                    userAgent.Mobile, 
                    userAgent.Robot,
                    userAgent.IsMobile, 
                    userAgent.IsBrowser, 
                    userAgent.IsRobot, 
                    watch.ElapsedMilliseconds,
                    (short)context.Response.StatusCode));

                return Task.CompletedTask;
            });
            return _next(context);
        }
    }
}