using System;

namespace Doodor.Utils.Timing
{
    public interface IClock
    {
        DateTimeOffset LocalNowWithOffset();
        DateTimeOffset UtcNowWithOffset();
        DateTime LocalNow();
        DateTime UtcNow();
    }
}