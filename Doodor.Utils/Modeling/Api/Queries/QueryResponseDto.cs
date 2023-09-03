namespace Doodor.Utils.Utilities.Modeling.Api.Queries
{
    public abstract class QueryResponseDto : Dto
    {
        protected QueryResponseDto()
        {
            Status = QueryStatus.OK;
        }

        public virtual QueryStatus Status { get; set; }
        public virtual BadRequestDto BadRequestReason { get; set; }
    }
}