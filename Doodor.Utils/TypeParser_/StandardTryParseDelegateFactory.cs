using Doodor.Utils.ExceptionHandling;
using Doodor.Utils.Utilities.ExceptionHandling;
using System;
using System.Linq;
using System.Reflection;
using static Doodor.Utils.ExceptionHandling.Exceptions;

namespace Doodor.Utils.Utilities
{
    internal static class StandardTryParseDelegateFactory
    {
        public static Delegate CreateTryParseDelegateFromType(Type parsableType)
        {
            if (parsableType == null)
                throw ArgNullEx(nameof(parsableType));

            const string TryParse = nameof(TryParse);
            const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Static;
            MethodInfo tryParseMethodInfo;

            if (parsableType.IsEnum)
            {
                var enumTryParseMethodInfo = typeof(Enum)
                    .GetMethods(bindingFlags)
                    .Single(x => x.Name.Equals(TryParse) && x.GetParameters().Length.Equals(2));

                tryParseMethodInfo = enumTryParseMethodInfo.MakeGenericMethod(parsableType);
            }
            else
            {
                var byRefType = parsableType.MakeByRefType();
                var signatureTypes = new[] { typeof(string), byRefType };

                tryParseMethodInfo = parsableType.GetMethod(TryParse, bindingFlags, null, signatureTypes, null);
            }

            if (tryParseMethodInfo == null)
                throw new DoodorUtilitiesException(
                    "public static method with signature " +
                    $"\"System.Boolean TryParse(System.String, out {parsableType})\" not found in type '{parsableType}'");

            var tryParseDelegate = Delegate.CreateDelegate(
                typeof(StandardTryParse<>).MakeGenericType(parsableType), tryParseMethodInfo);

            return tryParseDelegate;
        }

        public static StandardTryParse<T> CreateTryParseDelegateFromType<T>() =>
            (StandardTryParse<T>)CreateTryParseDelegateFromType(typeof(T));
    }
}