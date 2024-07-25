using Interfaces.Models.Messages;

namespace View.Messages;

public sealed class AmarokNearby : IMessage
{
    public string GetMessage() => "[red]You can smell the rotten stench of an amarok in a nearby room.[/]";
}