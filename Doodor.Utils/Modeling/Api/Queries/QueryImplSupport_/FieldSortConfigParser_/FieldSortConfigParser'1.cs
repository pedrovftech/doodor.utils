using Doodor.Utils.Utilities.Collections;
using System.Collections.Generic;
using System.Linq;
using static Doodor.Utils.ExceptionHandling.Exceptions;

namespace Doodor.Utils.Utilities.Modeling.Api.Queries
{
    public class FieldSortConfigParser<TField>
    {
        private readonly ITypeParser<TField> _fieldParser;
        private readonly IEqualityComparer<TField> _equalityComparer;
        private readonly bool _hasEqualityComparer;
        private readonly HashSet<TField> _expectedFields;

        public FieldSortConfigParser(
            TField fieldDefaultValue,
            IEnumerable<TField> expectedFields,
            ITypeParser<TField> fieldParser,
            IEqualityComparer<TField> equalityComparer)
        {
            if (expectedFields == null) throw ArgNullEx(nameof(expectedFields));

            _fieldParser = fieldParser ?? throw ArgNullEx(nameof(fieldParser));
            _equalityComparer = equalityComparer;
            _hasEqualityComparer = _equalityComparer != null;
            _expectedFields = _hasEqualityComparer
                ? new HashSet<TField>(expectedFields, _equalityComparer)
                : new HashSet<TField>(expectedFields);

            if (_expectedFields.Contains(fieldDefaultValue))
                throw ArgEx(
                    nameof(expectedFields),
                    $"'{nameof(expectedFields)}' cannot contain '{nameof(fieldDefaultValue)}'");
        }

        public FieldSortConfigParser(
            TField fieldDefaultValue,
            IEnumerable<TField> expectedFields,
            ITypeParser<TField> fieldParser) :
                this(fieldDefaultValue, expectedFields, fieldParser, null)
        { }

        public FieldSortConfigParseResult<TField> Parse(IEnumerable<string> fieldTexts)
        {
            if (fieldTexts == null) throw ArgNullEx(nameof(fieldTexts));

            var allFieldSortConfigs = fieldTexts
                .Select((fieldText, specifiedOrder) => Parse(fieldText, specifiedOrder))
                    .ToReadOnlyList();


            var hasFailedFieldParsings = allFieldSortConfigs.Any(x => x.FieldParsingFailed);
            var hasUnexpectedFields = allFieldSortConfigs.Any(x => x.UnexpectedField);
            var hasUndefinedSortDirections = allFieldSortConfigs.Any(x => x.UndefinedDirection);

            if (!hasFailedFieldParsings)
                allFieldSortConfigs
                    .GroupBy(x => x.Field)
                    .Where(x => x.Count() > 1)
                        .ForEach(x => x.ForEach(y => y.NonUniqueField = true));


            var hasNonUniqueFields =
                !hasFailedFieldParsings &&
                allFieldSortConfigs.Any(x => x.NonUniqueField);


            var succeeded =
                !hasFailedFieldParsings &&
                !hasUnexpectedFields &&
                !hasUndefinedSortDirections &&
                !hasNonUniqueFields;


            Dictionary<TField, FieldSortConfig<TField>> indexedFieldSortConfigs;

            if (succeeded)
            {
                indexedFieldSortConfigs = _hasEqualityComparer
                    ? allFieldSortConfigs.ToDictionary(x => x.Field, x => x, _equalityComparer)
                    : allFieldSortConfigs.ToDictionary(x => x.Field, x => x);
            }
            else
            {
                indexedFieldSortConfigs = new Dictionary<TField, FieldSortConfig<TField>>();
            }

            var res = new FieldSortConfigParseResult<TField>(
                succeeded,
                hasFailedFieldParsings,
                hasUnexpectedFields,
                hasUndefinedSortDirections,
                hasNonUniqueFields,
                allFieldSortConfigs,
                indexedFieldSortConfigs,
                _equalityComparer);


            return res;
        }

        public FieldSortConfigParseResult<TField> Parse(params string[] fieldTexts) =>
            Parse((IEnumerable<string>)fieldTexts);

        private FieldSortConfig<TField> Parse(string fieldText, int specifiedOrder)
        {
            var fieldParsingFailed = (fieldText?.Length ?? 0) < 2;

            FieldSortConfig<TField> fieldParsingFailedRes() =>
                new FieldSortConfig<TField>(
                    default(TField), fieldText, specifiedOrder,
                    true, false, FieldSortDirection.Undefined);

            if (fieldParsingFailed)
                return fieldParsingFailedRes();

            var fieldNameText = fieldText.Substring(0, fieldText.Length - 1);
            fieldParsingFailed = !_fieldParser.TryParse(fieldNameText, out var field);

            if (fieldParsingFailed)
                return fieldParsingFailedRes();

            var unexpectedField = !_expectedFields.Contains(field);

            var fieldSortText = fieldText.Last();
            var isAscDir = fieldSortText == '+';
            var isDescDir = fieldSortText == '-';
            var sortDir = FieldSortDirection.Undefined;

            if (isAscDir) sortDir = FieldSortDirection.Asc;
            else if (isDescDir) sortDir = FieldSortDirection.Desc;

            return new FieldSortConfig<TField>(
                field, fieldText, specifiedOrder,
                fieldParsingFailed, unexpectedField, sortDir);
        }
    }
}