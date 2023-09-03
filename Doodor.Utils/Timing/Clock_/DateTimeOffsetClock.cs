using System;
using static Doodor.Utils.ExceptionHandling.Exceptions;

namespace Doodor.Utils.Timing
{
    public class DateTimeOffsetClock : IClock
    {
        private readonly IUtcToOtherTimeZoneConverter _utcToOtherTimeZoneConverter;
        public DateTimeOffsetClock(IUtcToOtherTimeZoneConverter utcToOtherTimeZoneConverter)
        {
            _utcToOtherTimeZoneConverter = utcToOtherTimeZoneConverter ??
                throw ArgNullEx(nameof(utcToOtherTimeZoneConverter));
        }

        public DateTime LocalNow() =>
            _utcToOtherTimeZoneConverter.FromUtcToOther(DateTimeOffset.UtcNow).DateTime;

        public DateTime UtcNow() => DateTimeOffset.UtcNow.DateTime;

        public DateTimeOffset LocalNowWithOffset() =>
            _utcToOtherTimeZoneConverter.FromUtcToOther(DateTimeOffset.UtcNow);

        public DateTimeOffset UtcNowWithOffset() => DateTimeOffset.UtcNow;

    }
}