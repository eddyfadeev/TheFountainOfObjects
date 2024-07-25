using Interfaces.Controller;
using Interfaces.Models.Repository;
using Interfaces.Services.Factories;
using Interfaces.Services.GameSettings;
using Model.Player;
using Services.Extensions;
using Shared;
using Shared.Enums.Views.Factory;
using Shared.Enums.Views.Messages;
using View.Views.CreatePlayerScreen;

namespace Controller.Handlers;

public class PlayerHandler : IPlayerHandler
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IGameSettingsManager _gameSettingsManager;
    private readonly IMenuHandler _menuHandler;
    private readonly IMessageFactory _messageFactory;
    private readonly IMessagesHandler _messagesHandler;
    
    public PlayerHandler(
        IPlayerRepository playerRepository, 
        IGameSettingsManager gameSettingsManager,
        IMenuHandler menuHandler,
        IMessageFactory messageFactory,
        IMessagesHandler messagesHandler
    )
    {
        _playerRepository = playerRepository;
        _gameSettingsManager = gameSettingsManager;
        _menuHandler = menuHandler;
        _messageFactory = messageFactory;
        _messagesHandler = messagesHandler;
    }
    
    public bool CreatePlayer()
    {
        var playerName = GetValidUserName();

        InitializeNewPlayer(playerName);
        ShowPlayerCreatedMessage();
        
        return true;
    }
    public string GetValidUserName()
    {
        Console.Clear();
        
        var createPlayerScreen = new CreatePlayerScreen(_messageFactory);

        while (true)
        {
            var playerName = createPlayerScreen.AskForUserName();

            if (CheckIfPlayerExists(playerName))
            {
                _messagesHandler.ShowMessage(MessageType.PlayerAlreadyExistsMessage);
            }
            else
            {
                return playerName;
            }
        }
    }
    
    public bool CheckIfPlayerExists(string playerName)
    {
        try
        {
            _playerRepository.LoadPlayer(playerName);
        }
        catch (ArgumentException)
        {
            return false;
        }

        return true;
    }
    
    public bool TryLoadPlayer()
    {
        var userChoice = _menuHandler.ShowMenu(MenuType.LoadPlayerMenu);
        
        if (userChoice is null)
        {
            return false;
        }
        
        var player = _playerRepository.LoadPlayer(userChoice.ToString());
        _playerRepository.Player = player.ToDomain(_gameSettingsManager);

        return true;
    }
    
    private void InitializeNewPlayer(string playerName)
    {
        _playerRepository.Player = new Player(_gameSettingsManager)
        {
            Name = playerName,
            Score = 0,
            Location = new Location()
        };
    }
    
    private void ShowPlayerCreatedMessage()
    {
        _messagesHandler.ShowMessage(MessageType.PlayerCreatedMessage);
        _messagesHandler.ShowMessage(MessageType.PressAnyKeyToContinue);
        Console.ReadKey();
    }
}