using System;

namespace Doodor.Utils.Timing
{
    public class ElapsedTimeResult
    {
        public ElapsedTimeResult(DateTimeOffset start, DateTimeOffset end, TimeSpan elapsed)
        {
            Start = start;
            End = end;
            Elapsed = elapsed;
        }

        public DateTimeOffset Start { get; private set; }
        public DateTimeOffset End { get; private set; }
        public TimeSpan Elapsed { get; private set; }
    }
}