using Model;
using Model.Player;
using Services.Database.Interfaces;
using Services.Extensions;
using Spectre.Console;
using View.Enums;
using View.Views.CreatePlayerMenu;
using View.Views.CreatePlayerScreen;

namespace Controller;

internal class MenuHandler
{
    private readonly IMenuCommandFactory _menuCommandFactory;
    private readonly IPlayerRepository _playerRepository;

    public MenuHandler(IMenuCommandFactory menuCommandFactory, IPlayerRepository playerRepository)
    {
        _menuCommandFactory = menuCommandFactory;
        _playerRepository = playerRepository;
    }

    public void ShowStartScreen() => ShowMenu(MenuType.StartScreen);
    
    public void ShowCreatePlayerMenu()
    {
        var isRunning = true;
        
        while (isRunning)
        {
            var userChoice = ShowMenu(MenuType.CreatePlayerMenu);
            
            if (userChoice is CreatePlayerEntries.LoadPlayer)
            {
                
                isRunning = !TryLoadPlayer();
            }
            else
            {
                isRunning = !CreatePlayerScreen();
            }
        }
    }
    
    public Enum? ShowMainMenu() => ShowMenu(MenuType.MainMenu);
    
    public void ShowSettingsMenu() => ShowMenu(MenuType.SettingsMenu);
    
    public void ShowLeaderboardMenu() => ShowMenu(MenuType.LeaderboardMenu);
    
    public void ShowHelpMenu() => ShowMenu(MenuType.HelpMenu);

    private bool CreatePlayerScreen()
    {
        var createPlayerScreen = new CreatePlayerScreen();
        
        Console.Clear();
        
        do
        {
            var player = createPlayerScreen.AskForUserName();

            try
            {
                _playerRepository.LoadPlayer(player);
            }
            catch (ArgumentException)
            {
                _playerRepository.Player = 
                    new Player
                {
                    Name = player,
                    Score = 0,
                    Location = new Location()
                };
                
                createPlayerScreen.ShowPlayerCreatedMessage();

                return true;
            }
            createPlayerScreen.ShowAlreadyExistsMessage();

        } while (true);
    }

    private bool TryLoadPlayer()
    {
        var userChoice = ShowMenu(MenuType.LoadPlayerMenu);
        
        if (userChoice is null)
        {
            return false;
        }
        
        var player = _playerRepository.LoadPlayer(userChoice.ToString());
        _playerRepository.Player = player.ToDomain();

        return true;
    }

    private Enum? ShowMenu(MenuType commandType)
    {
        var command = _menuCommandFactory.Create(commandType);
        
        if (commandType is
            MenuType.CreatePlayerMenu or
            MenuType.LoadPlayerMenu or
            MenuType.SettingsMenu or
            MenuType.MainMenu)
        {
            var userChoice = command.Execute();
            
            return userChoice;
        }
        else
        {
            command.Execute();
            
            return MenuType.Back;
        }
    }
    
    
}