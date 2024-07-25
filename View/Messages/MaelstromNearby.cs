using Interfaces.Models.Messages;

namespace View.Messages;

public sealed class MaelstromNearby : IMessage
{
    public string GetMessage() => "[cyan1]You hear the growling and groaning of a maelstrom nearby.[/]";
}