using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.API.ViewModels.Common;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Notifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Filters
{
    public class ResponseConsistenceFilter : IAsyncResultFilter
    {
        private readonly DomainNotificationContext _domainNotificationContext;
        public ResponseConsistenceFilter(DomainNotificationContext domainNotificationContext)
        {
            _domainNotificationContext = domainNotificationContext;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var apiResponse = ApiResponseContract.From(_domainNotificationContext.Notifications);

            if (context.Result != null)
            {
                switch (context.Result)
                {
                    case OkObjectResult ok:
                        apiResponse.SetData(ok.Value);

                        if (!apiResponse.Success)
                            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                        break;
                    case BadRequestObjectResult _:
                        apiResponse.SetData(null);
                        break;
                }
            }

            var json = JsonConvert.SerializeObject(apiResponse.ToJson(), new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc
            });

            context.HttpContext.Response.ContentType = "application/json";
            await context.HttpContext.Response.WriteAsync(json);

            if (!context.HttpContext.Response.HasStarted)
                await next();
        }
    }
}