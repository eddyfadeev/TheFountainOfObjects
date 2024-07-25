using Interfaces.Models.Messages;

namespace View.Messages;

public sealed class MissedShot : IMessage
{
    public string GetMessage() => "[white]You missed a shot![/]";
}