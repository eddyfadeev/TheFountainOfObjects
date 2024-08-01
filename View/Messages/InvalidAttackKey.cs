using Interfaces.Models.Messages;

namespace View.Messages;

public sealed class InvalidAttackKey : IMessage
{
    public string GetMessage() => "[red]Invalid attack key. Please use arrow keys or WASD.[/]";
}