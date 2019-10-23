using System;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Security
{
    public interface ITokenProvider
    {
        string CreateNumericToken();
        string CreateAlphaNumericToken();
        string CreateAlphaNumericTokenWithExpiration(int expirationInDays);
        DateTime ExtractExpirationDateTimeFromToken(string token);
    }
}