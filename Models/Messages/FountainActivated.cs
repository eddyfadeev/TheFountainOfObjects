using Model.Messages.Interfaces;

namespace Model.Messages;

public class FountainActivated : IMessage
{
    public string GetMessage() => "[white]You activated the fountain![/]";
}