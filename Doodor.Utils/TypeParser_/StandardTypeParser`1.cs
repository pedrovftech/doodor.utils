using System;

namespace Doodor.Utils.Utilities
{
    public class StandardTypeParser<T> : ITypeParser<T>
    {
        private readonly StandardTryParse<T> _typeStandardTryParse;

        public StandardTypeParser() =>
            _typeStandardTryParse =
                StandardTryParseDelegateFactory.CreateTryParseDelegateFromType<T>();

        public Type TargetType => typeof(T);

        public bool TryParse(string value, out T result) =>
            _typeStandardTryParse(value, out result);

        public bool TryParse(string value, out object result)
        {
            var succeeded = TryParse(value, out T typedRes);
            result = typedRes;
            return succeeded;
        }
    }
}