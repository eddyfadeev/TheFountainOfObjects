using Model.Messages.Interfaces;

namespace Model.Messages;

public sealed class MissedShot : IMessage
{
    public string GetMessage() => "[white]You missed a shot![/]";
}