using Interfaces.Models.Messages;

namespace View.Messages;

public sealed class SteppedIntoAmarok : IMessage
{
    public string GetMessage() => "[red]Oops! You've been eaten by an amarok.[/]";
}