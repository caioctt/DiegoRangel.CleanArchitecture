using System.Collections.Generic;
using System.Linq;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Notifications;

namespace DiegoRangel.DotNet.Framework.CQRS.API.ViewModels.Common
{
    public class ApiResponseContract
    {
        public bool Success { get; private set; }
        public object Data { get; private set; }
        public string[] Errors { get; private set; }

        public static ApiResponseContract From(object data, IReadOnlyCollection<DomainNotification> errors)
        {
            var success = errors == null || errors.Count == 0;

            return new ApiResponseContract
            {
                Success = success,
                Data = success ? data : null,
                Errors = errors?.Select(x => x.Message).ToArray()
            };
        }

        public object ToJson()
        {
            return new
            {
                success = Success,
                data = Data,
                errors = Errors,
            };
        }
    }
}