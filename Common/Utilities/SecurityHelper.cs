using System;
using System.Security.Cryptography;
using System.Text;

namespace Common.Utilities
{
    public static class SecurityHelper
    {
        public static string GetHash(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                var inputValue = Encoding.UTF8.GetBytes(input);
                var hashValue = sha256.ComputeHash(inputValue);
                return Convert.ToBase64String(hashValue);
            }
        }
    }
}