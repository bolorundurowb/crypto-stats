using System;

namespace crypto_stats.Utils.Extensions
{
    public static class DateTimeExtensions
    {
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1);
        
        public static long ToUnixTimeStamp(this DateTime dateTime)
        {
            var timeSpan = dateTime - UnixEpoch;
            return (long) timeSpan.TotalMilliseconds;
        }
    }
}
