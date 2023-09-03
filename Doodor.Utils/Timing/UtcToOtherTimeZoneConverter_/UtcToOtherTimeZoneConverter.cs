using System;
using static Doodor.Utils.ExceptionHandling.Exceptions;

namespace Doodor.Utils.Timing
{
    public class UtcToOtherTimeZoneConverter : IUtcToOtherTimeZoneConverter
    {
        private readonly TimeZoneInfo _utcTimeZone = TimeZoneInfo.Utc;
        private readonly TimeZoneInfo _otherTimeZone;

        public UtcToOtherTimeZoneConverter(TimeZoneInfo otherTimeZone) =>
            _otherTimeZone = otherTimeZone ?? throw ArgNullEx(nameof(otherTimeZone));


        public DateTimeOffset FromUtcToOther(DateTimeOffset value)
        {
            if (value.Offset != TimeSpan.Zero)
                throw new Exception($"'{nameof(value)}' must be UTC.");

            var converted = TimeZoneInfo.ConvertTime(value, _otherTimeZone);
            return converted;
        }

        public DateTimeOffset FromOtherToUtc(DateTimeOffset value)
        {
            if (value.Offset == TimeSpan.Zero)
                throw new Exception($"'{nameof(value)}' must not be UTC.");

            var converted = TimeZoneInfo.ConvertTime(value, _utcTimeZone);
            return converted;
        }
    }
}
