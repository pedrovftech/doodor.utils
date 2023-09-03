using System;

namespace Doodor.Utils.Timing
{
    public class FrozenClock : IClock
    {
        private readonly DateTimeOffset _timePoint;
        public FrozenClock(DateTimeOffset timePoint) => _timePoint = timePoint;
        public DateTime LocalNow() => _timePoint.DateTime;
        public DateTime UtcNow() => _timePoint.DateTime;
        public DateTimeOffset LocalNowWithOffset() => _timePoint;
        public DateTimeOffset UtcNowWithOffset() => _timePoint;
    }
}