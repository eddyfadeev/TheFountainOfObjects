using Model.Messages.Interfaces;

namespace Model.Messages;

public class NothingHappened : IMessage
{
    public string GetMessage() => "Nothing happened.";
}