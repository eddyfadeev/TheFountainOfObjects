using Model.Messages.Interfaces;

namespace Model.Messages;

public class AttackDirection : IMessage
{
    public string GetMessage() => "[white]Choose a direction to attack.[/]";
}