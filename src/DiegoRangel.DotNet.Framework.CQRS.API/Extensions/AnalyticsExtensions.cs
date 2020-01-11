using DiegoRangel.DotNet.Framework.CQRS.API.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Extensions
{
    public static class AnalyticsExtensions
    {
        public static void UseAnalytics(this IApplicationBuilder app)
        {
            app.UseMiddleware<ResponseTimeMiddleware>();
        }
    }
}