using System;
using System.Text;
using App.Waes.ScalableWeb.Application.Contract;

namespace App.Waes.ScalableWeb.Infrastructure.Service
{
    public class CryptographyHandler : ICryptographyHandler
    {
        public string Encode(string content)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(content);
            return Convert.ToBase64String(plainTextBytes);
        }

        public string Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}