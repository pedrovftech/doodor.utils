namespace Doodor.Utils.Utilities.Modeling.Api.Commands
{
    public abstract class CommandResponseDto : Dto
    {
        protected CommandResponseDto()
        {
            Status = CommandStatus.OK;
        }

        public virtual CommandStatus Status { get; set; }
        public virtual BadRequestDto BadRequestReason { get; set; }
    }
}