using Interfaces.Models.Messages;

namespace Model.Messages;

public class AttackDirection : IMessage
{
    public string GetMessage() => "[white]Choose a direction to attack.[/]";
}