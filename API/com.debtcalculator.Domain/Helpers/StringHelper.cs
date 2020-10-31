using System;
using System.Security.Cryptography;
using System.Text;

namespace com.debtcalculator.Domain.Helpers
{
    public static class StringHelper
    {
        public static string Encrypt(this string password, string salt)
        {
            var arrayBytes = Encoding.UTF8.GetBytes(password + salt);
            byte[] hashBytes;
            using (var sha = SHA512.Create())
            {
                hashBytes = sha.ComputeHash(arrayBytes);
            }
            return Convert.ToBase64String(hashBytes);
        }

        public static string GenerateRandomSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            var key = new byte[32];
            rng.GetBytes(key);
            var sb = new StringBuilder();
            foreach (var item in key)
            {
                sb.Append(item.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}