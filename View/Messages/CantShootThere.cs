using Interfaces.Models.Messages;

namespace View.Messages;

public class CantShootThere : IMessage
{
    public string GetMessage() => "[white]There is a wall, you can't shoot there.[/]";
}