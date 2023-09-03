using System.Collections.Generic;

namespace Doodor.Utils.Utilities.Modeling.Api.Queries
{
    public class StringFieldSortConfigParser : FieldSortConfigParser<string>
    {
        private static readonly ITypeParser<string> FieldParser =
            new StringTypeParser();

        private static readonly IEqualityComparer<string> CaseInsensitiveEqualityComparer =
            new CaseInsensitiveStringEqualityComparer();


        public StringFieldSortConfigParser(
            string defaultFieldValue,
            IEnumerable<string> expectedFields,
            IEqualityComparer<string> equalityComparer) :
                base(
                    defaultFieldValue, expectedFields,
                    FieldParser, equalityComparer)
        { }


        public StringFieldSortConfigParser(
            string defaultFieldValue,
            IEnumerable<string> expectedFields) :
                base(
                    defaultFieldValue, expectedFields,
                    FieldParser, CaseInsensitiveEqualityComparer)
        { }
    }
}