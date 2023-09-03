using Dapper;

namespace Doodor.Utils.Utilities.Modeling.Api.Queries
{
    public class WhereClauseAndParams
    {
        public WhereClauseAndParams(
            bool wasFilled, string whereClause, DynamicParameters parameters)
        {
            WasFilled = wasFilled;
            WhereClause = whereClause;
            Parameters = parameters;
        }

        public bool WasFilled { get; private set; }
        public string WhereClause { get; private set; }
        public DynamicParameters Parameters { get; private set; }
    }
}