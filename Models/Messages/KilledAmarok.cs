using Model.Messages.Interfaces;

namespace Model.Messages;

public sealed class KilledAmarok : IMessage
{
    public string GetMessage() => "[green]You have killed the amarok![/]";
}