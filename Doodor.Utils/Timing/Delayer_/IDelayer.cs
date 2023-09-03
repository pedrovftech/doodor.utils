using System;
using System.Threading;
using System.Threading.Tasks;

namespace Doodor.Utils.Timing
{
    public interface IDelayer
    {
        Task DelayAsync(TimeSpan time, CancellationToken cancellationToken);
    }
}