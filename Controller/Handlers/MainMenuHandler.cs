using Interfaces.Controller;
using Shared.Enums.Views.Factory;
using Shared.Enums.Views.Menus;

namespace Controller.Handlers;

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
    
    public Enum? ShowMainMenu() => _menuHandler.ShowMenu(MenuType.MainMenu);
    
    public void ShowLeaderboardMenu() => _menuHandler.ShowMenu(MenuType.LeaderboardMenu);
    
    public void ShowHelpMenu() => _menuHandler.ShowMenu(MenuType.HelpMenu);
    
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
}