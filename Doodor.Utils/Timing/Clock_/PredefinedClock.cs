using Doodor.Utils.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using static Doodor.Utils.ExceptionHandling.Exceptions;

namespace Doodor.Utils.Timing
{
    public class PredefinedClock : IClock
    {
        private readonly Queue<DateTimeOffset> _timePoints;
        public PredefinedClock(
            IEnumerable<DateTimeOffset> timePoints)
        {
            if (timePoints == null) throw ArgNullEx(nameof(timePoints));
            if (!timePoints.Any()) throw ArgCannotBeEmptyEx(nameof(timePoints));

            _timePoints = new Queue<DateTimeOffset>(timePoints);
        }

        public PredefinedClock(
            params DateTimeOffset[] timePoints)
            : this((IEnumerable<DateTimeOffset>)timePoints) { }

        public DateTime LocalNow() =>
            Now().DateTime;

        public DateTimeOffset LocalNowWithOffset() => Now();

        public DateTimeOffset Now() =>
            _timePoints.Count > 0
                ? _timePoints.Dequeue()
                : throw new DoodorUtilitiesException("Out of configured time points.");

        public DateTime UtcNow() => Now().DateTime;

        public DateTimeOffset UtcNowWithOffset() => Now().ToUniversalTime();
    }
}