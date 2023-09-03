using System;

namespace Doodor.Utils.Utilities.Modeling.Api.Commands
{
    public abstract class CommandRequestDto : Dto
    {
        public virtual Guid CorrelationId { get; set; } = Guid.NewGuid();
    }
}