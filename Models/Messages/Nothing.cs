using Model.Messages.Interfaces;

namespace Model.Messages;

public sealed class Nothing : IMessage
{
    public string GetMessage() => "You feel nothing.";
}