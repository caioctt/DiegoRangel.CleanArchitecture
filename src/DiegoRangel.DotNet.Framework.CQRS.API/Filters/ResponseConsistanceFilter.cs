using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.API.ViewModels.Common;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Notifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Filters
{
    public class ResponseConsistanceFilter : IAsyncResultFilter
    {
        private readonly DomainNotificationContext _domainNotificationContext;
        public ResponseConsistanceFilter(DomainNotificationContext domainNotificationContext)
        {
            _domainNotificationContext = domainNotificationContext;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (context.Result != null)
            {
                ApiResponseContract apiResponse = null;

                switch (context.Result)
                {
                    case OkObjectResult ok:
                        apiResponse = ApiResponseContract.From(ok.Value, _domainNotificationContext.Notifications);

                        if (!apiResponse.Success)
                            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                        break;
                    case BadRequestObjectResult _:
                        apiResponse = ApiResponseContract.From(null, _domainNotificationContext.Notifications);
                        break;
                }
                
                context.HttpContext.Response.ContentType = "application/json";
                await context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(apiResponse?.ToJson()));
            }
            
            await next();
        }
    }
}