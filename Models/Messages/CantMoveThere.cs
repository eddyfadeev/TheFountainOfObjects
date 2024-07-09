using Interfaces.Models.Messages;

namespace Model.Messages;

public class CantMoveThere : IMessage
{
    public string GetMessage() => "[white]There is a wall, you can't move there.[/]";
}