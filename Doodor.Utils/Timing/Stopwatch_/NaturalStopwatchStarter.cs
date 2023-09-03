using System;
using static Doodor.Utils.ExceptionHandling.Exceptions;

namespace Doodor.Utils.Timing
{
    public class NaturalStopwatchStarter : IStopwatchStarter
    {
        private readonly IClock _clock;

        public NaturalStopwatchStarter(IClock clock) => _clock = clock ?? throw ArgNullEx(nameof(clock));

        public IStopwatch Start() => new NaturalStopwatch(_clock);

        private class NaturalStopwatch : StopwatchBase
        {
            private readonly IClock _clock;
            private readonly DateTimeOffset _start;

            public NaturalStopwatch(IClock clock)
            {
                _clock = clock ?? throw ArgNullEx(nameof(clock));
                _start = _clock.UtcNow();
            }

            public override ElapsedTimeResult ElapsedTime()
            {
                var end = _clock.UtcNow();
                var elapsed = end - _start;
                var result = new ElapsedTimeResult(_start, end, elapsed);

                return result;
            }
        }
    }
}