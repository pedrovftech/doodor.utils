using System;
using System.Threading;
using System.Threading.Tasks;

namespace Doodor.Utils.Timing
{
    public class TaskDelayer : IDelayer
    {
        public Task DelayAsync(TimeSpan time, CancellationToken cancellationToken) =>
            Task.Delay(time, cancellationToken);
    }
}