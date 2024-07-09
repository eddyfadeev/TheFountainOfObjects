using Interfaces.Models.Messages;

namespace View.Messages;

public sealed class AtActiveFountain : IMessage
{
    public string GetMessage() => "[deepskyblue1]You hear the rushing waters from the Fountain of Objects. " +
                                  "It has been reactivated![/]";
}