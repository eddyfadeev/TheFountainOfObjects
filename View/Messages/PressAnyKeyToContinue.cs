using Interfaces.Models.Messages;

namespace View.Messages;

public sealed class PressAnyKeyToContinue : IMessage
{
    public string GetMessage() => "[white]Press any key to continue...[/]";
}