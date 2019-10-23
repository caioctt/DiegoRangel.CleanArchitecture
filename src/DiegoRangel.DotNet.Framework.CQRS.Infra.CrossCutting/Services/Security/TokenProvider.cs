using System;
using System.Linq;
using System.Text;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Security
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IRandomizeProvider _randomizeProvider;
        public TokenProvider(IRandomizeProvider randomizeProvider)
        {
            _randomizeProvider = randomizeProvider;
        }

        public string CreateNumericToken()
        {
            var generator = new Random();
            return generator.Next(100000, 999999).ToString();
        }

        public string CreateAlphaNumericToken()
        {
            var key = Encoding.ASCII.GetBytes(_randomizeProvider.GenerateUniqueKey());
            var token = Convert.ToBase64String(key);

            return token;
        }

        public string CreateAlphaNumericTokenWithExpiration(int expirationInDays)
        {
            var dateTime = BitConverter.GetBytes(DateTime.Now.AddDays(expirationInDays).ToBinary());
            var key = Encoding.ASCII.GetBytes(_randomizeProvider.GenerateUniqueKey());
            var token = Convert.ToBase64String(dateTime.Concat(key).ToArray());

            return token;
        }

        public DateTime ExtractExpirationDateTimeFromToken(string token)
        {
            byte[] data = Convert.FromBase64String(token);
            return DateTime.FromBinary(BitConverter.ToInt64(data, 0));
        }
    }
}