using System;
using System.Collections.Generic;
using static Doodor.Utils.ExceptionHandling.Exceptions;

namespace Doodor.Utils.Utilities.Collections
{
    internal class ParsableKeyValuePairs : IParsableKeyValuePairs
    {
        private readonly IReadOnlyStringKeyValuePairs _target;
        private readonly IDictionary<Type, ITypeParser> _typeParsers;

        // 'IReadOnlyStringKeyValuePairs' decouples the implementation from 
        // a specific collection type. It enables cenarios such as the 
        // targeting of a .Net Core IConfiguration instance.

        public ParsableKeyValuePairs(
            IReadOnlyStringKeyValuePairs target, IDictionary<Type, ITypeParser> typeParsers)
        {
            _target = target ?? throw ArgNullEx(nameof(target));
            _typeParsers = typeParsers ?? throw ArgNullEx(nameof(typeParsers));
        }

        public ParsableKeyValuePairs(IReadOnlyStringKeyValuePairs keyValuePairs)
            : this(keyValuePairs, DefaultTypeParsers.CommonTypeParsersByType) { }


        public ITryGetAndParseResult<T> TryGetAndParse<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw ArgMustBeProvidedEx(nameof(key));

            var report = new TryGetAndParseResult<T> { Key = key };
            if (_target.CanCheckContainsKey && !((report.KeyDeclared = _target.ContainsKey(key)) ?? false)) return report;

            report.UnparsedValue = _target[key];
            if (!(report.ValueNonEmpty = !string.IsNullOrEmpty(report.UnparsedValue))) return report;
            report.KeyDeclared = true;

            var requireParsing = !typeof(T).Equals(typeof(string));
            if (!requireParsing)
            {
                report.ValueParsingSucceeded = true;
                report.ParsedValue = (T)(object)report.UnparsedValue;
                return report;
            }

            var parsingSucceeded = _typeParsers.Get<T>().TryParse(report.UnparsedValue, out var parsedValue);
            if (!(report.ValueParsingSucceeded = parsingSucceeded)) return report;

            report.ParsedValue = parsedValue;
            return report;
        }

        public T EnsureValue<T>(string key, Predicate<T> condition) =>
            TryGetAndParse<T>(key).EnsureParsedValue(condition);


        public T EnsureValue<T>(string key) =>
            EnsureValue<T>(key, _ => true);


        private class TryGetAndParseResult<T> : ITryGetAndParseResult<T>
        {
            public string Key { get; set; }
            public bool? KeyDeclared { get; set; }
            public bool ValueNonEmpty { get; set; }
            public bool ValueParsingSucceeded { get; set; }
            public string UnparsedValue { get; set; }
            public T ParsedValue { get; set; }

            public T EnsureParsedValue(Predicate<T> condition)
            {
                if (condition == null)
                    throw ArgNullEx(nameof(condition));

                var type = typeof(T);

                if (KeyDeclared.HasValue && !KeyDeclared.Value)
                    throw DoodorUtilEx(
                            $"Key '{Key}', whose value expected type is '{type}', is not declared.");

                if (!ValueNonEmpty)
                    throw DoodorUtilEx(KeyDeclared.HasValue && KeyDeclared.Value
                        ? $"Value associated with key '{Key}', whose expected type is '{type}', is empty."
                        : $"Value associated with key '{Key}', whose expected type is '{type}', is not declared or empty.");

                if (!ValueParsingSucceeded)
                    throw DoodorUtilEx(
                        $"Value associated with key '{Key}' cannot be parsed as type '{type}' -- the unparsable value is '{UnparsedValue}'.");

                if (!condition(ParsedValue))
                    throw DoodorUtilEx(
                        $"Value associated with key '{Key}', parsed as type '{type}', is not valid -- the invalid parsed value is '{ParsedValue}'");

                return ParsedValue;
            }

            public T EnsureParsedValue() => EnsureParsedValue(_ => true);
        }
    }
}