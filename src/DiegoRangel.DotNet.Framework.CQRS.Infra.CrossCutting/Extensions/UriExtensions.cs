using System;
using System.Net;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Extensions
{
    public static class UriExtensions
    {
        public static string ToDecodedUri(this Uri uri)
        {
            var result = WebUtility.UrlDecode($"{uri.Scheme}://{uri.Host}:{uri.Port}{uri.AbsolutePath}?");
            var queryParams = uri.Query.FromQueryStringToDictionary();

            foreach (var (key, value) in queryParams)
            {
                result += $"{key}={WebUtility.UrlDecode(value)}&";
            }

            return result.Substring(0, result.Length - 1);
        }
    }
}