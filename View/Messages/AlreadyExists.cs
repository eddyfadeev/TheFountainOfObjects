using Interfaces.Models.Messages;

namespace View.Messages;

public sealed class AlreadyExistsMessage : IMessage
{
    public string GetMessage() => "[red]This name is already taken. Please, choose another one.[/]";
}