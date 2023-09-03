using System;
using System.Collections.Generic;
using System.Linq;
using static Doodor.Utils.ExceptionHandling.Exceptions;

namespace Doodor.Utils.Utilities.Modeling.Api.Queries
{
    public class FieldSortConfigParseResult<TField>
    {
        private readonly IEqualityComparer<TField> _equalityComparer;
        private readonly bool _hasEqualityComparer;

        public FieldSortConfigParseResult(
            bool succeeded,

            bool hasFailedFieldParsings,
            bool hasUnexpectedFields,
            bool hasUndefinedSortDirections,
            bool hasNonUniqueFields,

            IReadOnlyList<FieldSortConfig<TField>> allFieldSortConfigs,
            IReadOnlyDictionary<TField, FieldSortConfig<TField>> indexedFieldSortConfigs,

            IEqualityComparer<TField> equalityComparer)
        {
            Succeeded = succeeded;

            HasFailedFieldParsings = hasFailedFieldParsings;
            HasUnexpectedFields = hasUnexpectedFields;
            HasUndefinedSortDirections = hasUndefinedSortDirections;
            HasNonUniqueFields = hasNonUniqueFields;

            AllFieldSortConfigs = allFieldSortConfigs ?? throw ArgNullEx(nameof(allFieldSortConfigs));
            IndexedFieldSortConfigs = indexedFieldSortConfigs ?? throw ArgNullEx(nameof(indexedFieldSortConfigs));

            _equalityComparer = equalityComparer;
            _hasEqualityComparer = _equalityComparer != null;
        }


        public bool Succeeded { get; private set; }

        public bool HasFailedFieldParsings { get; private set; }
        public bool HasUnexpectedFields { get; private set; }
        public bool HasUndefinedSortDirections { get; private set; }
        public bool HasNonUniqueFields { get; private set; }

        public IReadOnlyList<FieldSortConfig<TField>> AllFieldSortConfigs { get; private set; }
        public IReadOnlyDictionary<TField, FieldSortConfig<TField>> IndexedFieldSortConfigs { get; private set; }


        public string BuildOrderByClause(IDictionary<TField, string> fieldMapper) =>
            BuildOrderByClauseInternal(fieldMapper ?? throw ArgNullEx(nameof(fieldMapper)));


        public string BuildOrderByClause() =>
            BuildOrderByClauseInternal(null);


        private string BuildOrderByClauseInternal(IDictionary<TField, string> fieldMapper)
        {
            if (!Succeeded)
                throw DoodorUtilEx(
                    "Cannot build order by clause if Succeeded is false.");

            if (!AllFieldSortConfigs.Any())
                return string.Empty;

            var hasFieldMapper = fieldMapper?.Any() ?? false;
            var localFieldMapper = hasFieldMapper && _hasEqualityComparer
                ? new Dictionary<TField, string>(fieldMapper, _equalityComparer)
                : fieldMapper;

            var mapField = hasFieldMapper
                ? (Func<TField, string>)(x => localFieldMapper[x])
                : x => x.ToString();

            var orderByFieldsAndDir = AllFieldSortConfigs
                .Select(x => $"{mapField(x.Field)} {x.Direction.ToString().ToUpper()}");

            var orderByClause = string.Join(", ", orderByFieldsAndDir);
            return orderByClause;
        }
    }
}