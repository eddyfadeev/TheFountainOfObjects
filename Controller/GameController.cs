using Model.Player;
using Services.Database.Interfaces;
using View.Enums;
using View.Views.CreatePlayerMenu;
using View.Views.Game;
using View.Views.MainMenu;

namespace Controller;

public class GameController
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IPlayerRepository _playerRepository;
    private readonly IGameSettingsRepository _gameSettingsRepository;
    private readonly MenuHandler _menuHandler;
    
    public GameController(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        var menuCommandFactory = _serviceProvider.GetRequiredService<IMenuCommandFactory>();
        _playerRepository = _serviceProvider.GetRequiredService<IPlayerRepository>();
        _gameSettingsRepository = _serviceProvider.GetRequiredService<IGameSettingsRepository>();
        _menuHandler = new MenuHandler(menuCommandFactory, _playerRepository, _gameSettingsRepository);
    }

    public void LaunchGame()
    {
        Console.CursorVisible = false;
        //Console.SetWindowSize(120, 35); 
        
        _menuHandler.ShowStartScreen();
        _menuHandler.ShowCreatePlayerMenu();

        do
        {
            var menuChoice = _menuHandler.ShowMainMenu();
        
            switch (menuChoice)
            {
                case MainMenuEntries.StartGame:
                    StartGame();
                    break;
                case MainMenuEntries.Leaderboard:
                    _menuHandler.ShowLeaderboardMenu();
                    break;
                case MainMenuEntries.Settings:
                    _menuHandler.ShowSettingsMenu();
                    break;
                case MainMenuEntries.Help:
                    _menuHandler.ShowHelpMenu();
                    break;
                case MainMenuEntries.Exit:
                    return;
            }
        } while (true);
        
        
    }

    private void StartGame()
    {
        var player = _playerRepository.Player;
        var gameView = _serviceProvider.GetRequiredService<IGameVIew>();

        do
        {
            gameView.Display();
            Console.ReadKey();
        } while (true);
    }
}