using Interfaces.Controller;
using Interfaces.Services.Factories;
using Shared.Enums.Views.Messages;
using Spectre.Console;

namespace Controller.Handlers;

public class MessagesHandler : IMessagesHandler
{
    private readonly IMessageFactory _messageFactory;
    
    public MessagesHandler(IMessageFactory messageFactory)
    {
        _messageFactory = messageFactory;
    }
    
    public void ShowMessage(MessageType message)
    {
        var messageToShow = _messageFactory.Create(message);
        AnsiConsole.MarkupLine(messageToShow.GetMessage());
    }
}