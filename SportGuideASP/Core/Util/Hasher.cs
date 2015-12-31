using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SportGuideASP.Core.Util
{
    public static class Hasher
    {
        public static string GetMD5Hash(string text)
        {
            return GetHash(MD5.Create(), text);
        }

        private static string GetHash(HashAlgorithm algorithm, string text)
        {
            using (algorithm)
            {
                byte[] bytes = algorithm.ComputeHash(Encoding.UTF8.GetBytes(text));
                return BitConverter
                    .ToString(bytes)
                    .Replace("-", "")
                    .ToLower();
            }
        }
    }
}