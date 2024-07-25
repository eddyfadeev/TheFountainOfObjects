using Interfaces.Models.Messages;

namespace View.Messages;

public sealed class KilledAmarok : IMessage
{
    public string GetMessage() => "[green]You have killed an amarok![/]";
}