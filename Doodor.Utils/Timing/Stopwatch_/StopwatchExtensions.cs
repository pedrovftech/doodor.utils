using System;
using System.Threading;
using System.Threading.Tasks;
using static Doodor.Utils.ExceptionHandling.Exceptions;

namespace Doodor.Utils.Timing
{
    public static class StopwatchExtensions
    {
        public static async Task DelayIfHasTimeRemaining(this
            IStopwatch @this, TimeSpan timeToReach,
            IDelayer delayer, CancellationToken cancellationToken)
        {
            if (@this == null) throw ArgNullEx(nameof(@this));
            if (delayer == null) throw ArgNullEx(nameof(delayer));

            var remainingRes = @this.RemaningTime(timeToReach);

            if (remainingRes.Remaining > TimeSpan.Zero)
                await delayer.DelayAsync(remainingRes.Remaining, cancellationToken);
        }
    }
}
