using Interfaces.Models.Messages;

namespace View.Messages;

public sealed class FeelNothing : IMessage
{
    public string GetMessage() => "You feel nothing else.";
}