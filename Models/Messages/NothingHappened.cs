using Interfaces.Models.Messages;

namespace Model.Messages;

public class NothingHappened : IMessage
{
    public string GetMessage() => "Nothing happened.";
}