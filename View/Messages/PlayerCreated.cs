using Interfaces.Models.Messages;

namespace View.Messages;

public sealed class PlayerCreated : IMessage
{
    public string GetMessage() => "[green]Player created[/]";
}