using System;
using DiegoRangel.DotNet.Framework.CQRS.API.Middlewares;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Analytics.Commands;
using Microsoft.AspNetCore.Builder;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Extensions
{
    public static class AnalyticsExtensions
    {
        public static void UseAnalytics(this IApplicationBuilder app, Action<TrackResponseCommand> onTrackAction)
        {
            app.UseMiddleware<ResponseTimeMiddleware>(onTrackAction);
        }
    }
}