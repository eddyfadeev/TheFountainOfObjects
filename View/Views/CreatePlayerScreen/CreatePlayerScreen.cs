using Interfaces.Models.Messages;
using Interfaces.Services.Factories;
using Shared.Enums.Views.Messages;

namespace View.Views.CreatePlayerScreen;

public class CreatePlayerScreen
{
    private readonly IMessage _enterNameMessage;

    public CreatePlayerScreen(IMessageFactory messageFactory)
    {
        _enterNameMessage = messageFactory.Create(MessageType.EnterNameMessage);
    }
    
    public string AskForUserName() => GetUserInput( _enterNameMessage.GetMessage());
}

