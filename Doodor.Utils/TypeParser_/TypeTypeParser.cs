using System;

namespace Doodor.Utils.Utilities
{
    public class TypeTypeParser : ITypeParser<Type>
    {
        public Type TargetType => typeof(Type);

        public bool TryParse(string value, out Type result)
        {
            result = Type.GetType(value, false);
            return result != null;
        }

        public bool TryParse(string value, out object result)
        {
            var succeeded = TryParse(value, out Type typedRes);
            result = typedRes;
            return succeeded;
        }
    }
}