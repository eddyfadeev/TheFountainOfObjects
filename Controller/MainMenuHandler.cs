using Interfaces.Models.Database;
using Interfaces.Models.GameSettings;
using Interfaces.View.Factory;
using Model.Player;
using Services.Extensions;
using Shared;
using Shared.Enums.Views.Factory;
using Shared.Enums.Views.Menus;
using View.Views.CreatePlayerScreen;
using View.Views.SettingsMenu;

namespace Controller;

internal class MainMenuHandler
{
    private readonly IMenuCommandFactory _menuCommandFactory;
    private readonly IPlayerRepository _playerRepository;
    private readonly IGameSettingsRepository _gameSettingsRepository;

    public MainMenuHandler(
        IMenuCommandFactory menuCommandFactory, 
        IPlayerRepository playerRepository, 
        IGameSettingsRepository gameSettingsRepository
        )
    {
        _menuCommandFactory = menuCommandFactory;
        _playerRepository = playerRepository;
        _gameSettingsRepository = gameSettingsRepository;
    }

    public void ShowStartScreen() => ShowMenu(CommandType.StartScreen);
    
    public void ShowCreatePlayerMenu()
    {
        var isRunning = true;
        
        while (isRunning)
        {
            var userChoice = ShowMenu(CommandType.CreatePlayerMenu);
            
            if (userChoice is PLayerInitMenuEntries.LoadPlayer)
            {
                
                isRunning = !TryLoadPlayer();
            }
            else
            {
                isRunning = !CreatePlayerScreen();
            }
        }
    }
    
    public Enum? ShowMainMenu() => ShowMenu(CommandType.MainMenu);
    
    public void ShowSettingsMenu()
    {
        const int maxDangerous = 3;
        const int maxArrows = 5;

        do
        {
            var userChoice = ShowMenu(CommandType.SettingsMenu);
            
            if (userChoice is SettingsMenuEntries.Back)
            {
                break;
            }

            switch (userChoice)
            {
                case SettingsMenuEntries.Amaroks:
                    _gameSettingsRepository.AmaroksCount = GetUserInput("Enter the number of Amaroks (0-3): ", maxDangerous);
                    break;
                case SettingsMenuEntries.Pits:
                    _gameSettingsRepository.PitsCount = GetUserInput("Enter the number of Pits (0-3): ", maxDangerous);
                    break;
                case SettingsMenuEntries.Maelstroms:
                    _gameSettingsRepository.MaelstromsCount = GetUserInput("Enter the number of Maelstroms (0-3): ", maxDangerous);
                    break;
                case SettingsMenuEntries.Arrows:
                    _gameSettingsRepository.ArrowsCount = GetUserInput("Enter the number of Arrows (0-5): ", maxArrows);
                    break;
                case SettingsMenuEntries.FieldSize:
                    var newMazeSize = SettingsMenuView.AskForMazeSize();
                    _gameSettingsRepository.SetMazeSize(newMazeSize);
                    break;
                case SettingsMenuEntries.ChangePlayerName:
                    _playerRepository.Player!.Name = ProcessUserNameInput();
                    break;
            }

            userChoice = null;
        } while (true);
    }

    public void ShowLeaderboardMenu() => ShowMenu(CommandType.LeaderboardMenu);
    
    public void ShowHelpMenu() => ShowMenu(CommandType.HelpMenu);

    private bool CheckIfPlayerExists(string player)
    {
        try
        {
            _playerRepository.LoadPlayer(player);
        }
        catch (ArgumentException)
        {
            return false;
        }

        return true;
    }

    private string ProcessUserNameInput()
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
    
    private bool CreatePlayerScreen()
    {
        var playerName = ProcessUserNameInput();
        
        _playerRepository.Player = 
            new Player (_gameSettingsRepository)
            {
                Name = playerName,
                Score = 0,
                Location = new Location()
            };
        
        return true;
    }

    private bool TryLoadPlayer()
    {
        var userChoice = ShowMenu(CommandType.LoadPlayerMenu);
        
        if (userChoice is null)
        {
            return false;
        }
        
        var player = _playerRepository.LoadPlayer(userChoice.ToString());
        _playerRepository.Player = player.ToDomain(_gameSettingsRepository);

        return true;
    }

    private Enum? ShowMenu(CommandType commandType)
    {
        var command = _menuCommandFactory.Create(commandType);
        
        if (commandType is
            CommandType.CreatePlayerMenu or
            CommandType.LoadPlayerMenu or
            CommandType.SettingsMenu or
            CommandType.MainMenu)
        {
            var userChoice = command.Execute();
            
            return userChoice;
        }
        else
        {
            command.Execute();
            
            return null;
        }
    }
}