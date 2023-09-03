using System.Collections.Generic;

namespace Doodor.Utils.Utilities
{
    public class CaseInsensitiveStringEqualityComparer : IEqualityComparer<string>
    {
        public bool Equals(string x, string y) =>
            string.Compare(x, y, ignoreCase: true) == 0;

        public int GetHashCode(string obj) =>
            obj.ToLower().GetHashCode();
    }
}