using System;

namespace Doodor.Utils.Timing
{
    public interface IStopwatch
    {
        ElapsedTimeResult ElapsedTime();
        RemainingTimeResult RemaningTime(TimeSpan to);
    }
}