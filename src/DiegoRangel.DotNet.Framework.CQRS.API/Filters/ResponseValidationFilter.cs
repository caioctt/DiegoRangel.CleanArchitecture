using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.API.ViewModels.Common;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.NotificationPattern;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Filters
{
    public class ResponseValidationFilter : IAsyncResultFilter
    {
        private readonly INotificationContext _notificationContext;
        public ResponseValidationFilter(INotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var apiResponse = ApiResponseContract.From(_notificationContext.Notifications);

            if ((context.Result is OkObjectResult
                 || context.Result is NoContentResult
                 || context.Result is CreatedResult) 
                && !apiResponse.Success)
            {
                var json = JsonConvert.SerializeObject(apiResponse.ToJson(), new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });

                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.HttpContext.Response.ContentType = "application/json";
                await context.HttpContext.Response.WriteAsync(json);
            }

            if (!context.HttpContext.Response.HasStarted)
                await next();
        }
    }
}