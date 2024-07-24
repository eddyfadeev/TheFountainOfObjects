using Interfaces.Controller;
using Interfaces.Models.Repository;
using Interfaces.Services.GameSettings;
using Model.Player;
using Services.Extensions;
using Shared;
using Shared.Enums.Views.Factory;
using View.Views.CreatePlayerScreen;

namespace Controller.Handlers;

public class PlayerHandler : IPlayerHandler
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IGameSettingsManager _gameSettingsManager;
    private readonly IMenuHandler _menuHandler;
    
    public PlayerHandler(
        IPlayerRepository playerRepository, 
        IGameSettingsManager gameSettingsManager,
        IMenuHandler menuHandler
    )
    {
        _playerRepository = playerRepository;
        _gameSettingsManager = gameSettingsManager;
        _menuHandler = menuHandler;
    }
    
    public bool CreatePlayer()
    {
        var playerName = ProcessUserNameInput();
        
        _playerRepository.Player = 
            new Player (_gameSettingsManager)
            {
                Name = playerName,
                Score = 0,
                Location = new Location()
            };
        
        return true;
    }
    public string ProcessUserNameInput()
    {
        Console.Clear();
        
        var createPlayerScreen = new CreatePlayerScreen();
        
        do
        {
            var playerName = createPlayerScreen.AskForUserName();

            if (CheckIfPlayerExists(playerName))
            {
                createPlayerScreen.ShowAlreadyExistsMessage();
            }
            else
            {
                createPlayerScreen.ShowPlayerCreatedMessage();

                return playerName;
            }
        } while (true);
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
}