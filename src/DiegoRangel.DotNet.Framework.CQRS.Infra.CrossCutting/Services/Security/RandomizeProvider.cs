using System.Security.Cryptography;
using System.Text;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Security
{
    public class RandomizeProvider : IRandomizeProvider
    {
        public string GenerateUniqueKey(int maxSize = 30)
        {
            const string str = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890-_";
            var chars = str.ToCharArray();
            var data = new byte[maxSize];

            var crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);

            var result = new StringBuilder(maxSize);

            foreach (var b in data)
                result.Append(chars[b % chars.Length]);

            return result.ToString();
        }
    }
}