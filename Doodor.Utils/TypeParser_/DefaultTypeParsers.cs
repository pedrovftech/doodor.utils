using System;
using System.Collections.Generic;
using System.Linq;
using static Doodor.Utils.ExceptionHandling.Exceptions;


namespace Doodor.Utils.Utilities
{
    public static class DefaultTypeParsers
    {
        private static readonly IEnumerable<Type> CommonTypes =
            new[]
            {
                typeof(bool),

                typeof(byte),
                typeof(sbyte),

                typeof(short),
                typeof(int),
                typeof(long),

                typeof(ushort),
                typeof(uint),
                typeof(ulong),

                typeof(float),
                typeof(double),
                typeof(decimal),

                typeof(char),

                typeof(TimeSpan),

                typeof(DateTime),
                typeof(DateTimeOffset),
            };


        private static readonly IDictionary<Type, ITypeParser> CommonTypeParsersByTypeInt = CommonTypes
            .Select(x => (key: x, value: CreateFromType(x)))
            .Union(new[] { (key: typeof(Type), value: (ITypeParser)new TypeTypeParser()) })
                .ToDictionary(x => x.key, x => x.value);


        public static IDictionary<Type, ITypeParser> CommonTypeParsersByType =>
            new Dictionary<Type, ITypeParser>(CommonTypeParsersByTypeInt);

        public static ITypeParser CreateFromType(Type parsableType)
        {
            if (parsableType == null)
                throw ArgNullEx(nameof(parsableType));

            var typeParserType = typeof(StandardTypeParser<>).MakeGenericType(parsableType);
            var typeParserInst = (ITypeParser)Activator.CreateInstance(typeParserType);

            return typeParserInst;
        }

        public static ITypeParser<T> CreateFromType<T>() => new StandardTypeParser<T>();
    }
}