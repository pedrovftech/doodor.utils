using System;

namespace Doodor.Utils.Timing
{
    public interface IUtcToOtherTimeZoneConverter
    {
        DateTimeOffset FromUtcToOther(DateTimeOffset value);
        DateTimeOffset FromOtherToUtc(DateTimeOffset value);
    }
}