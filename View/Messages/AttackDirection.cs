using Interfaces.Models.Messages;

namespace View.Messages;

public class AttackDirection : IMessage
{
    public string GetMessage() => "[white]Choose a direction to attack.[/]";
}