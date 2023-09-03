using System;

namespace Doodor.Utils.Utilities
{
    public class StringTypeParser : ITypeParser<string>
    {
        public Type TargetType => typeof(string);

        public bool TryParse(string value, out string result)
        {
            result = value;
            return true;
        }

        public bool TryParse(string value, out object result)
        {
            result = value;
            return true;
        }
    }
}