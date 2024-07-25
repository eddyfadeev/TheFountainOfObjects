using Interfaces.Models.Messages;

namespace View.Messages;

public sealed class PitNearby : IMessage
{
    public string GetMessage() => "[white]You feel a draft. There is a pit in a nearby room![/]";
}