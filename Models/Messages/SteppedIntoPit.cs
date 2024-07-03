using Model.Messages.Interfaces;

namespace Model.Messages;

public sealed class SteppedIntoPit : IMessage
{
    public string GetMessage() => "[red]Oops! You fell into a pit and died.[/]";
}