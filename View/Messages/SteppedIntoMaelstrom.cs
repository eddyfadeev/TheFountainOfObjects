using Interfaces.Models.Messages;

namespace View.Messages;

public sealed class SteppedIntoMaelstrom : IMessage
{
    public string GetMessage() => "[cyan1]Oops! You stepped into a maelstrom and being moved to a different room.[/]";
}