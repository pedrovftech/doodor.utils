using Doodor.Utils.ExceptionHandling;
using Doodor.Utils.Utilities.Collections;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using static Doodor.Utils.ExceptionHandling.Exceptions;

namespace Doodor.Utils.Configuration
{
    internal class ConfigurationKeyValuePairsAdapter : IReadOnlyStringKeyValuePairs
    {
        private readonly IConfiguration _target;
        private readonly HashSet<string> _keys;

        public ConfigurationKeyValuePairsAdapter(IConfiguration target, bool canCheckContainsKey)
        {
            _target = target ?? throw ArgNullEx(nameof(target));
            CanCheckContainsKey = canCheckContainsKey;

            if (canCheckContainsKey)
                _keys = new HashSet<string>(_target.AsEnumerable().Select(x => x.Key));
        }

        public string this[string key] => _target[key];

        public bool CanCheckContainsKey { get; private set; }
        public bool ContainsKey(string key) => CanCheckContainsKey
            ? _keys.Contains(key)
            : throw new DoodorUtilitiesException($"'{nameof(CanCheckContainsKey)}' is disabled.");
    }
}