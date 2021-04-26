using System;
using System.Net.Http;

namespace crypto_stats.Models.Extensions
{
    public static class ExceptionExtensions
    {
        public static bool IsInternetConnectionException(this Exception exception)
        {
            var messageChecks = exception.Message.Contains("The Internet connection appears to be offline")
                                || exception.Message.Contains(
                                    "A server with the specified hostname could not be found.")
                                || exception.Message.Contains("No address associated with hostname");

            if (exception is HttpRequestException httpRequestException)
            {
                return messageChecks || !httpRequestException.Message.ToLower().Contains("404");
            }

            return messageChecks;
        }
    }
}
