using Dapper;
using Doodor.Utils.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using static Doodor.Utils.ExceptionHandling.Exceptions;

namespace Doodor.Utils.Utilities.Modeling.Api.Queries
{
    public class WhereClauseAndParamsBuilder
    {
        private readonly List<WhereClausePart> _whereClauseParts;
        private readonly DynamicParameters _parameters;

        public WhereClauseAndParamsBuilder(DynamicParameters parameters)
        {
            _whereClauseParts = new List<WhereClausePart>();
            _parameters = parameters ?? throw ArgNullEx(nameof(parameters));
        }

        public WhereClauseAndParamsBuilder() :
            this(new DynamicParameters())
        { }

        public WhereClauseAndParamsBuilder AddPart(
            Func<bool> checkPreCondition,
            Func<string> buildSqlConditionPart,
            Action<DynamicParameters> addParams)
        {
            if (checkPreCondition == null) throw ArgNullEx(nameof(checkPreCondition));
            if (buildSqlConditionPart == null) throw ArgNullEx(nameof(buildSqlConditionPart));
            if (addParams == null) throw ArgNullEx(nameof(addParams));

            _whereClauseParts.Add(new WhereClausePart
            {
                CheckPreCondition = checkPreCondition,
                BuildSqlConditionPart = buildSqlConditionPart,
                AddParams = addParams
            });

            return this;
        }

        public WhereClauseAndParams Build()
        {
            var whereClausePartsWithTrueCondition = _whereClauseParts
                .Where(x => x.CheckPreCondition())
                    .ToReadOnlyCollection();

            if (!whereClausePartsWithTrueCondition.Any())
                return new WhereClauseAndParams(false, string.Empty, _parameters);

            var whereClause = string.Join(" AND ", whereClausePartsWithTrueCondition
                .Select(x => x.BuildSqlConditionPart()));

            foreach (var wherePart in whereClausePartsWithTrueCondition)
                wherePart.AddParams(_parameters);

            return new WhereClauseAndParams(true, whereClause, _parameters);
        }

        private class WhereClausePart
        {
            public Func<bool> CheckPreCondition { get; set; }
            public Func<string> BuildSqlConditionPart { get; set; }
            public Action<DynamicParameters> AddParams { get; set; }
        }
    }
}