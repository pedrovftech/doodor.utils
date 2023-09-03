namespace Doodor.Utils.Utilities.Modeling.Api.Commands
{
    public static class CommandStatusExtensions
    {
        public static bool Succeeded(this CommandStatus @this) =>
            @this == CommandStatus.OK ||
            @this == CommandStatus.Created ||
            @this == CommandStatus.Accepted ||
            @this == CommandStatus.NoContent;
    }
}