using Interfaces.Models.Messages;

namespace Model.Messages;

public sealed class Victory : IMessage
{
    public string GetMessage() => "[green]Congratulations! The Fountain of Objects has been reactivated" +
                                  "and you you have escaped the maze![/]";
}