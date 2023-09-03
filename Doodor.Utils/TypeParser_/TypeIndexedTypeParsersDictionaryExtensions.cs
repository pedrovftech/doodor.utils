using Doodor.Utils.ExceptionHandling;
using System;
using System.Collections.Generic;
using static Doodor.Utils.ExceptionHandling.Exceptions;

namespace Doodor.Utils.Utilities
{
    public static class TypeIndexedTypeParsersDictionaryExtensions
    {
        public static bool TryGet<T>(this
            IDictionary<Type, ITypeParser> @this, out ITypeParser<T> value)
        {
            if (@this == null) throw ArgNullEx(nameof(@this));

            var succeeded = @this.TryGetValue(typeof(T), out var untypedValue);
            value = (ITypeParser<T>)untypedValue;

            return succeeded;
        }


        public static ITypeParser<T> Get<T>(this
            IDictionary<Type, ITypeParser> @this) =>
                @this.TryGet<T>(out var value)
                    ? value
                    : throw new DoodorUtilitiesException($"TypeParser for type '{typeof(T)}' was not found.");
    }  
}