using Interfaces.Models.Messages;

namespace View.Messages;

public sealed class AtNotActiveFountain : IMessage
{
    public string GetMessage() => "[deepskyblue1]You hear water dripping in this room. " +
                                  "The Fountain of Objects is here![/]";
}