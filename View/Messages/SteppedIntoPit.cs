using Interfaces.Models.Messages;

namespace View.Messages;

public sealed class SteppedIntoPit : IMessage
{
    public string GetMessage() => "[red]Oops! You fell into a pit and died.[/]";
}