using System;

namespace WebLogin.Controllers.Shared
{
    public class Helpers
    {
        /// <summary>
        /// Encode given string using SHA1 algorithm
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string EncodeSHA1(string value)
        {
            var hash = System.Security.Cryptography.SHA1.Create();
            var encoder = new System.Text.ASCIIEncoding();
            var combined = encoder.GetBytes(value ?? "");
            return BitConverter.ToString(hash.ComputeHash(combined)).ToLower().Replace("-", "");
        }
    }
}