using Interfaces.Models.Messages;

namespace View.Messages;

public class FountainActivated : IMessage
{
    public string GetMessage() => "[white]You activated the fountain![/]";
}