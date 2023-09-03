using System;

namespace Doodor.Utils.Utilities.Collections
{
    public interface ITryGetAndParseResult<T>
    {
        string Key { get; }
        bool? KeyDeclared { get; }

        bool ValueNonEmpty { get; }
        bool ValueParsingSucceeded { get; }

        string UnparsedValue { get; }

        T ParsedValue { get; }
        T EnsureParsedValue(Predicate<T> condition);
        T EnsureParsedValue();
    }
}