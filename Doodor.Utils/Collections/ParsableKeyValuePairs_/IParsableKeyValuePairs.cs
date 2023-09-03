using System;

namespace Doodor.Utils.Utilities.Collections
{
    public interface IParsableKeyValuePairs
    {
        T EnsureValue<T>(string key);
        T EnsureValue<T>(string key, Predicate<T> condition);
        ITryGetAndParseResult<T> TryGetAndParse<T>(string key);
    }
}