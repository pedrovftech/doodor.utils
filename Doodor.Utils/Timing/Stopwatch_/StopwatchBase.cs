using System;

namespace Doodor.Utils.Timing
{
    public abstract class StopwatchBase : IStopwatch
    {
        public abstract ElapsedTimeResult ElapsedTime();

        public RemainingTimeResult RemaningTime(TimeSpan to)
        {
            var elapsedRes = ElapsedTime();
            var remaining = to - elapsedRes.Elapsed;
            var remainingRes = new RemainingTimeResult(
                elapsedRes.Start, elapsedRes.End, elapsedRes.Elapsed, remaining);

            return remainingRes;
        }
    }
}