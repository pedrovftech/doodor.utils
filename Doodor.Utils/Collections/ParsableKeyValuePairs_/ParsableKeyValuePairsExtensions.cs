using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using static Doodor.Utils.ExceptionHandling.Exceptions;

namespace Doodor.Utils.Utilities.Collections
{
    public static class ParsableKeyValuePairsExtensions
    {
        // Exposing Adapters.

        public static IParsableKeyValuePairs ToParsable(this IDictionary<string, string> @this,
            IDictionary<Type, ITypeParser> typeParsers) =>
                new ParsableKeyValuePairs(
                    new StringDictionaryKeyValuePairsAdapter(@this ?? throw ArgNullEx(nameof(@this))),
                    typeParsers);


        public static IParsableKeyValuePairs ToParsable(this IDictionary<string, string> @this) =>
            new ParsableKeyValuePairs(
                new StringDictionaryKeyValuePairsAdapter(
                    @this ?? throw ArgNullEx(nameof(@this))));


        public static IParsableKeyValuePairs ToParsable(this NameValueCollection @this,
            IDictionary<Type, ITypeParser> typeParsers) =>
                new ParsableKeyValuePairs(
                    new NameValueCollectionKeyValuePairsAdapter(@this ?? throw ArgNullEx(nameof(@this))),
                    typeParsers);


        public static IParsableKeyValuePairs ToParsable(this NameValueCollection @this) =>
            new ParsableKeyValuePairs(
                new NameValueCollectionKeyValuePairsAdapter(@this ?? throw ArgNullEx(nameof(@this))));



        // Exposing Decorators.

        public static IParsableKeyValuePairs NamespaceWith(this IParsableKeyValuePairs @this,
            string nmspace, bool prefix) =>
                new NamespacedParsableKeyValuePairs(
                    @this ?? throw ArgNullEx(nameof(@this)),
                    string.IsNullOrEmpty(nmspace) ? throw ArgMustBeProvidedEx(nameof(nmspace)) : nmspace,
                    prefix);


        public static IParsableKeyValuePairs LinkTo(this IParsableKeyValuePairs @this,
            IParsableKeyValuePairs other) =>
                new LinkedParsableKeyValuePairs(
                    @this ?? throw ArgNullEx(nameof(@this)),
                    other ?? throw ArgNullEx(nameof(other)));


        public static IParsableKeyValuePairs LinkTo(this IParsableKeyValuePairs @this,
            IParsableKeyValuePairs other, Predicate<string> ignoreLinked, Predicate<string> straightToLinked) =>
                new LinkedParsableKeyValuePairs(
                    @this ?? throw ArgNullEx(nameof(@this)),
                    other ?? throw ArgNullEx(nameof(other)),
                    ignoreLinked ?? throw ArgNullEx(nameof(ignoreLinked)),
                    straightToLinked ?? throw ArgNullEx(nameof(straightToLinked)));


        // Templating

        public static IParsableKeyValuePairs ApplyTemplating(this
            IParsableKeyValuePairs @this,
            string keyValuePairsName, IParsableKeyValuePairs parentKvps,
            string isTemplateKey, string templateIdKey,
            string namespaceSeparator, Predicate<string> ignoreLinked)
        {
            if (@this == null) throw ArgNullEx(nameof(@this));
            if (string.IsNullOrEmpty(keyValuePairsName)) throw ArgMustBeProvidedEx(nameof(keyValuePairsName));
            if (parentKvps == null) throw ArgNullEx(nameof(parentKvps));
            if (string.IsNullOrEmpty(isTemplateKey)) throw ArgMustBeProvidedEx(nameof(isTemplateKey));
            if (string.IsNullOrEmpty(templateIdKey)) throw ArgMustBeProvidedEx(nameof(templateIdKey));
            if (ignoreLinked == null) throw ArgMustBeProvidedEx(nameof(ignoreLinked));

            ITryGetAndParseResult<string> templateIdTgpRes = null;
            bool straightToLinked(string _) => false;

            var settingsKvps = @this;
            IParsableKeyValuePairs possibiblyTemplatableKvps = settingsKvps;

            while ((templateIdTgpRes = possibiblyTemplatableKvps.TryGetAndParse<string>(templateIdKey)).KeyDeclared ?? false)
            {
                var templateId = templateIdTgpRes.EnsureParsedValue();
                var templateKvps = parentKvps
                    .NamespaceWith($"{templateId}{namespaceSeparator}", prefix: true);

                if (!templateKvps.EnsureValue<bool>(isTemplateKey))
                    throw DoodorUtilEx(
                        $"Keys namespaced with {templateIdKey} '{templateId}' " +
                        $"aren't flagged with {isTemplateKey} true.");

                settingsKvps = settingsKvps.LinkTo(templateKvps, ignoreLinked, straightToLinked);
                possibiblyTemplatableKvps = templateKvps;
            }

            return settingsKvps;
        }


        // Adapters

        private class StringDictionaryKeyValuePairsAdapter : IReadOnlyStringKeyValuePairs
        {
            private readonly IDictionary<string, string> _target;

            public StringDictionaryKeyValuePairsAdapter(IDictionary<string, string> target) => _target = target;

            public string this[string key] => _target[key];

            public bool CanCheckContainsKey { get; } = true;
            public bool ContainsKey(string key) => _target.ContainsKey(key);
        }


        private class NameValueCollectionKeyValuePairsAdapter : IReadOnlyStringKeyValuePairs
        {
            private readonly NameValueCollection _target;
            private readonly HashSet<string> _keys;

            public NameValueCollectionKeyValuePairsAdapter(NameValueCollection target)
            {
                // Args validated in the factory method
                _target = target;
                _keys = new HashSet<string>(_target.Keys.Cast<string>());
            }

            public string this[string key] => _target[key];

            public bool CanCheckContainsKey { get; } = true;
            public bool ContainsKey(string key) => _keys.Contains(key);
        }


        // Decorators

        private class LinkedParsableKeyValuePairs : IParsableKeyValuePairs
        {
            private readonly IParsableKeyValuePairs _a;
            private readonly IParsableKeyValuePairs _b;
            private readonly Predicate<string> _ignoreLinked;
            private readonly Predicate<string> _straightToLinked;

            public LinkedParsableKeyValuePairs(
                IParsableKeyValuePairs a, IParsableKeyValuePairs b,
                Predicate<string> ignoreLinked, Predicate<string> straightToLinked)
            {
                // Args validated in the factory method
                _a = a;
                _b = b;
                _ignoreLinked = ignoreLinked;
                _straightToLinked = straightToLinked;
            }

            public LinkedParsableKeyValuePairs(IParsableKeyValuePairs a, IParsableKeyValuePairs b)
                : this(a, b, _ => false, _ => false) { }

            public ITryGetAndParseResult<T> TryGetAndParse<T>(string key)
            {
                if (string.IsNullOrEmpty(key))
                    throw ArgMustBeProvidedEx(nameof(key));

                if (_ignoreLinked(key)) return _a.TryGetAndParse<T>(key);
                if (_straightToLinked(key)) return _b.TryGetAndParse<T>(key);

                var aTgpRes = _a.TryGetAndParse<T>(key);
                if (aTgpRes.KeyDeclared.HasValue && aTgpRes.KeyDeclared.Value) return aTgpRes;

                var bTgpTgpRes = _b.TryGetAndParse<T>(key);
                return bTgpTgpRes;
            }

            public T EnsureValue<T>(string key, Predicate<T> condition) => TryGetAndParse<T>(key).EnsureParsedValue(condition);
            public T EnsureValue<T>(string key) => EnsureValue<T>(key, _ => true);
        }



        private class NamespacedParsableKeyValuePairs : IParsableKeyValuePairs
        {
            private readonly IParsableKeyValuePairs _target;
            private readonly bool _prefix;

            public NamespacedParsableKeyValuePairs(IParsableKeyValuePairs target, string nmspace, bool prefix)
            {
                // Args validated in the factory method.
                _target = target;
                Namespace = nmspace;
                _prefix = prefix;
            }

            public string Namespace { get; private set; }

            public T EnsureValue<T>(string key) => _target.EnsureValue<T>(ApplyNamespace(key));
            public T EnsureValue<T>(string key, Predicate<T> condition) =>
                _target.EnsureValue(ApplyNamespace(key), condition);


            public ITryGetAndParseResult<T> TryGetAndParse<T>(string key) =>
                _target.TryGetAndParse<T>(ApplyNamespace(key));


            private string ApplyNamespace(string key)
            {
                if (string.IsNullOrEmpty(key)) throw ArgMustBeProvidedEx(nameof(key));
                var nmspacedKey = _prefix ? Namespace + key : key + Namespace;
                return nmspacedKey;
            }
        }
    }
}