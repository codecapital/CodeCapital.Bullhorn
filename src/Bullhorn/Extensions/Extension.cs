using System;

namespace CodeCapital.Bullhorn.Extensions
{
    public static class Extension
    {
        public static DateTime ToDateTime(this long timestamp)
            => DateTimeOffset.FromUnixTimeMilliseconds(timestamp).DateTime;

        public static long Timestamp(this DateTime datetime)
            => new DateTimeOffset(datetime).ToUnixTimeMilliseconds();
    }
}
