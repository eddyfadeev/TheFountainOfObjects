using Model.Messages.Interfaces;

namespace Model.Messages;

public sealed class NoArrowsLeft : IMessage
{
    public string GetMessage() => "[red]You don't have any arrows left.[/]";
}