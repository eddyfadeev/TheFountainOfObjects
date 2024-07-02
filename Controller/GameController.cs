using Model.Enums;
using Model.GameSettings;
using Model.Interfaces;
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
    private readonly IMazeService<IRoom> _mazeService;
    private readonly IGameView _gameView;
    
    public GameController(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        var menuCommandFactory = _serviceProvider.GetRequiredService<IMenuCommandFactory>();
        _playerRepository = _serviceProvider.GetRequiredService<IPlayerRepository>();
        _gameSettingsRepository = _serviceProvider.GetRequiredService<IGameSettingsRepository>();
        _menuHandler = new MenuHandler(menuCommandFactory, _playerRepository, _gameSettingsRepository);
        _mazeService = _serviceProvider.GetRequiredService<IMazeService<IRoom>>();
        _gameView = _serviceProvider.GetRequiredService<IGameView>();
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
    
    private (TypeOfAction actionType, Direction direction) ProcessKeyPress(ConsoleKey key)
    {
        if (!GameControlKeys.IsValidKey(key))
        {
            throw new ArgumentException("Invalid key pressed.");
        }

        var actionType = GameControlKeys.GetTypeOfAction(key);
        var direction = GameControlKeys.GetDirectionFromKey(key);
        
        return (actionType, direction);
    }

    private void MakeAnAction(TypeOfAction actionType, Direction direction)
    {
        var playerActionsHandler = new PlayerActionsHandler(_playerRepository, _mazeService, _gameView);
        switch (actionType)
        {
            case TypeOfAction.Move:
                playerActionsHandler.Move(direction);
                break;
            case TypeOfAction.Attack:
                // Attack
                break;
            case TypeOfAction.Interact:
                // Interact
                break;
            case TypeOfAction.Pause:
                // Pause game
                break;
            default:
                
                break;
        }
    }

    private void StartGame()
    {
        var gameView = _serviceProvider.GetRequiredService<IGameView>();
        var mazeGeneratorService = _serviceProvider.GetRequiredService<IMazeGeneratorService>();
        
        gameView.Display();
        do
        {
            var pressedKey = Console.ReadKey(true);
            var keypressData = ProcessKeyPress(pressedKey.Key);
            MakeAnAction(keypressData.actionType, keypressData.direction);
            
            var newMaze = mazeGeneratorService.UpdateTable();
            
            gameView.UpdateMaze(newMaze);
        } while (true);
    }
}