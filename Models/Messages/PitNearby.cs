using Model.Messages.Interfaces;

namespace Model.Messages;

public sealed class PitNearby : IMessage
{
    public string GetMessage() => "[grey11]You feel a draft. There is a pit in a nearby room![/]";
}