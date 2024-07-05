using Model.GameSettings;
using Model.Interfaces;
using Model.Maze;
using Model.Messages.Enum;
using Services.Database.Interfaces;
using View.Views.Game;
using View.Views.MainMenu;

namespace Controller;

public class GameController
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IPlayerRepository _playerRepository;
    private readonly IGameSettingsRepository _gameSettingsRepository;
    private readonly MenuHandler _menuHandler;
    private readonly IMaze<IRoom> _maze;
    private readonly IGameView _gameView;
    
    public GameController(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        var menuCommandFactory = _serviceProvider.GetRequiredService<IMenuCommandFactory>();
        _playerRepository = _serviceProvider.GetRequiredService<IPlayerRepository>();
        _gameSettingsRepository = _serviceProvider.GetRequiredService<IGameSettingsRepository>();
        _menuHandler = new MenuHandler(menuCommandFactory, _playerRepository, _gameSettingsRepository);
        _maze = _serviceProvider.GetRequiredService<IMaze<IRoom>>();
        _gameView = _serviceProvider.GetRequiredService<IGameView>();
    }

    public void LaunchGame()
    {
        Console.CursorVisible = false;
        
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
    
    private void ProcessKeyPress(ConsoleKey key)
    {
        var mazeService = _serviceProvider.GetRequiredService<IMazeService<IRoom>>();
        var actionType = GameControlKeys.GetTypeOfAction(key);
        var playerActionsHandler = new PlayerActionsHandler(_playerRepository, _maze, _gameView);
        
        switch (actionType)
        {
            case TypeOfAction.Move:
                var direction = GameControlKeys.GetDirectionFromKey(key);
                playerActionsHandler.Move(direction);
                break;
            case TypeOfAction.Attack:
                _gameView.UpdateSpecialMessage(MessageType.Attack);
                var newMaze = mazeService.UpdateMaze();
                
                _gameView.UpdateMaze(newMaze);
                var keyPress = Console.ReadKey(true);
                var directionToAttack = GameControlKeys.GetDirectionFromKey(keyPress.Key);

                playerActionsHandler.Attack(directionToAttack);
                break;
            case TypeOfAction.Interact:
                playerActionsHandler.InteractWithRoom(_playerRepository.Player!.Location);
                break;
            case TypeOfAction.Pause:
                // Pause game
                break;
            case TypeOfAction.DoNothing:
            default:
                return;
        }
        
    }

    private void StartGame()
    {
        var mazeService = _serviceProvider.GetRequiredService<IMazeService<IRoom>>();
        
        _gameView.Display();
        do
        {
            var pressedKey = Console.ReadKey(true);
            ProcessKeyPress(pressedKey.Key);
            
            var newMaze = mazeService.UpdateMaze();
            
            _gameView.UpdateMaze(newMaze);
        } while (true);
    }
}