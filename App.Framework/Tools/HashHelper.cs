using System.Security.Cryptography;
using System.Text;

namespace App.Framework.Tools
{
    public static class HashHelper
    {
        public static string ToMd5Hex(this string input)
        {
            if (input is null) throw new ArgumentNullException(nameof(input));

            using (var md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                var sb = new StringBuilder(hashBytes.Length * 2);
                foreach (var b in hashBytes)
                    sb.Append(b.ToString("x2"));

                return sb.ToString();
            }
        }
    }
}
