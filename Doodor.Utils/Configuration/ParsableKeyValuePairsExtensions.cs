using Doodor.Utils.Utilities;
using Doodor.Utils.Utilities.Collections;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using static Doodor.Utils.ExceptionHandling.Exceptions;

namespace Doodor.Utils.Configuration
{
    public static class ParsableKeyValuePairsExtensions
    {
        public static IParsableKeyValuePairs ToParsable(this IConfiguration @this,
            bool canCheckContainsKey, IDictionary<Type, ITypeParser> typeParsers) =>
                new ParsableKeyValuePairs(
                    new ConfigurationKeyValuePairsAdapter(@this ?? throw ArgNullEx(nameof(@this)), canCheckContainsKey),
                    typeParsers ?? throw ArgNullEx(nameof(typeParsers)));


        public static IParsableKeyValuePairs ToParsable(this IConfiguration @this,
            bool canCheckContainsKey) =>
                new ParsableKeyValuePairs(
                    new ConfigurationKeyValuePairsAdapter(@this ?? throw ArgNullEx(nameof(@this)), canCheckContainsKey));
    }
}