namespace Doodor.Utils.Utilities.Modeling.Api.Queries
{
    public static class QueryStatusExtensions
    {
        public static bool Succeeded(this QueryStatus @this) =>
            @this == QueryStatus.OK;
    }
}