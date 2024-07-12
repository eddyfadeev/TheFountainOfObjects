using Interfaces.Models.Messages;

namespace Model.Messages;

public sealed class KilledAmarok : IMessage
{
    public string GetMessage() => "[green]You have killed an amarok![/]";
}