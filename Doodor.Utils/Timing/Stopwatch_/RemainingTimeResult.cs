using System;

namespace Doodor.Utils.Timing
{
    public class RemainingTimeResult : ElapsedTimeResult
    {
        public RemainingTimeResult(
            DateTimeOffset start, DateTimeOffset end, TimeSpan elapsed, TimeSpan remaining)
                : base(start, end, elapsed)
        {
            Remaining = remaining;
        }

        public TimeSpan Remaining { get; private set; }
        public bool RemainingGtZero => Remaining > TimeSpan.Zero;
    }
}