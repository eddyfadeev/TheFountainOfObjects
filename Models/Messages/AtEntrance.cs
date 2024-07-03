using Model.Messages.Interfaces;

namespace View.Messages;

public sealed class AtEntrance : IMessage
{
    public string GetMessage() => "[gold3_1]You see light coming from the cavern entrance.[/]";
}