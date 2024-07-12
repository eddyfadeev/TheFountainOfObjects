using Interfaces.Models.Messages;

namespace Model.Messages;

public class FountainIsAlreadyActivated : IMessage
{
    public string GetMessage() => "[white]The fountain is already activated![/]";
}