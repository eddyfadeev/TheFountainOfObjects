using Interfaces.Models.Messages;

namespace View.Messages;

public class FountainIsAlreadyActivated : IMessage
{
    public string GetMessage() => "[white]The fountain is already activated![/]";
}