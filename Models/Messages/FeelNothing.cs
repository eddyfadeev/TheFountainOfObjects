using Interfaces.Models.Messages;

namespace Model.Messages;

public sealed class FeelNothing : IMessage
{
    public string GetMessage() => "You feel nothing else.";
}