using System.Globalization;
using Microsoft.Extensions.DependencyInjection;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Extensions
{
    public static class CultureExtensions
    {
        public static void AddCulture(this IServiceCollection services, string cultureName)
        {
            CultureInfo.CurrentCulture = new CultureInfo(cultureName);
        }
    }
}