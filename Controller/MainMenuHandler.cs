using Interfaces.Controller;
using Interfaces.Models.Repository;
using Interfaces.Services.GameSettings;
using Interfaces.View.Factory;
using Model.Player;
using Services.Extensions;
using Shared;
using Shared.Enums.Views.Factory;
using Shared.Enums.Views.Menus;
using View.Views.CreatePlayerScreen;

namespace Controller;

internal class MainMenuHandler
{
    private readonly IMenuHandler _menuHandler;
    private readonly ISettingsHandler _settingsHandler;
    private readonly IPlayerHandler _playerHandler;

    public MainMenuHandler(
        IMenuHandler menuHandler, 
        ISettingsHandler settingsHandler,
        IPlayerHandler playerHandler
        )
    {
        _menuHandler = menuHandler;
        _settingsHandler = settingsHandler;
        _playerHandler = playerHandler;
    }

    public void ShowStartScreen() => _menuHandler.ShowMenu(MenuType.StartScreen);
    
    public void ShowCreatePlayerMenu()
    {
        var isRunning = true;
        
        while (isRunning)
        {
            var userChoice = _menuHandler.ShowMenu(MenuType.CreatePlayerMenu);
            
            if (userChoice is PLayerInitMenuEntries.LoadPlayer)
            {
                
                isRunning = !_playerHandler.TryLoadPlayer();
            }
            else
            {
                isRunning = !_playerHandler.CreatePlayer();
            }
        }
    }
    
    public Enum? ShowMainMenu() => _menuHandler.ShowMenu(MenuType.MainMenu);
    
    public void ShowSettingsMenu()
    {
        do
        {
            var userChoice = _menuHandler.ShowMenu(MenuType.SettingsMenu);
            
            if (userChoice is SettingsMenuEntries.Back)
            {
                break;
            }

            if (userChoice is null)
            {
                Console.WriteLine("Invalid menu entry. Please try again.");
            }
            else
            {
                _settingsHandler.HandleSetting((SettingsMenuEntries)userChoice);
            }
        } while (true);
    }

    public void ShowLeaderboardMenu() => _menuHandler.ShowMenu(MenuType.LeaderboardMenu);
    
    public void ShowHelpMenu() => _menuHandler.ShowMenu(MenuType.HelpMenu);
}

public class MenuHandler : IMenuHandler
{
    private readonly IMenuCommandFactory _menuCommandFactory;
    
    public MenuHandler(IMenuCommandFactory menuCommandFactory)
    {
        _menuCommandFactory = menuCommandFactory;
    }
    
    public Enum? ShowMenu(MenuType menuType)
    {
        var command = _menuCommandFactory.Create(menuType);
        var userChoice = command.Execute();
        
        return userChoice;
    }
}

public interface IMenuHandler
{
    Enum? ShowMenu(MenuType menuType);
}

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

public interface IPlayerHandler
{
    bool TryLoadPlayer();
    bool CreatePlayer();
    bool CheckIfPlayerExists(string playerName);
    string ProcessUserNameInput();
}