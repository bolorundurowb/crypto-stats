using System;

namespace crypto_stats.Utils.Extensions
{
    public static class DateTimeExtensions
    {
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long ToUnixTimeStamp(this DateTime dateTime)
        {
            var timeSpan = dateTime - UnixEpoch;
            return (long) timeSpan.TotalMilliseconds;
        }

        public static DateTime FromUnixTimeStamp(this long unixTimestamp)
        {
            var dateTime = UnixEpoch + TimeSpan.FromMilliseconds(unixTimestamp);
            return dateTime.ToLocalTime();
        }
    }
}
