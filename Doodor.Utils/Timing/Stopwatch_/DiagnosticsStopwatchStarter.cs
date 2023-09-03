using System;
using System.Diagnostics;
using static Doodor.Utils.ExceptionHandling.Exceptions;

namespace Doodor.Utils.Timing
{
    public class DiagnosticsStopwatchStarter : IStopwatchStarter
    {
        private readonly IClock _clock;

        public DiagnosticsStopwatchStarter(IClock clock) =>
            _clock = _clock ?? throw ArgNullEx(nameof(clock));

        public IStopwatch Start() => new DiagnosticsStopwatch(_clock);

        private class DiagnosticsStopwatch : StopwatchBase
        {
            private readonly IClock _clock;
            private readonly Stopwatch _stopwatch;
            private readonly DateTimeOffset _start;

            public DiagnosticsStopwatch(IClock clock)
            {
                _clock = clock ?? throw ArgNullEx(nameof(clock));
                _stopwatch = Stopwatch.StartNew();
                _start = _clock.UtcNow();
            }

            public override ElapsedTimeResult ElapsedTime()
            {
                var elapsed = _stopwatch.Elapsed;
                var end = _clock.UtcNow();
                var result = new ElapsedTimeResult(_start, end, elapsed);

                return result;
            }
        }
    }
}