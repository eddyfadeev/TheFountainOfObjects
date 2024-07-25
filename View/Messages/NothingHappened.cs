using Interfaces.Models.Messages;

namespace View.Messages;

public class NothingHappened : IMessage
{
    public string GetMessage() => "Nothing happened.";
}