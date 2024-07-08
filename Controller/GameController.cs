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
    private readonly MainMenuHandler _mainMenuHandler;
    private readonly IMaze<IRoom> _maze;
    private readonly IGameView _gameView;
    
    public GameController(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        var menuCommandFactory = _serviceProvider.GetRequiredService<IMenuCommandFactory>();
        _playerRepository = _serviceProvider.GetRequiredService<IPlayerRepository>();
        _gameSettingsRepository = _serviceProvider.GetRequiredService<IGameSettingsRepository>();
        _mainMenuHandler = new MainMenuHandler(menuCommandFactory, _playerRepository, _gameSettingsRepository);
        _maze = _serviceProvider.GetRequiredService<IMaze<IRoom>>();
        _gameView = _serviceProvider.GetRequiredService<IGameView>();
    }

    public void LaunchGame()
    {
        Console.CursorVisible = false;
        
        _mainMenuHandler.ShowStartScreen();
        _mainMenuHandler.ShowCreatePlayerMenu();

        do
        {
            var menuChoice = _mainMenuHandler.ShowMainMenu();
        
            switch (menuChoice)
            {
                case MainMenuEntries.StartGame:
                    StartGame();
                    break;
                case MainMenuEntries.Leaderboard:
                    _mainMenuHandler.ShowLeaderboardMenu();
                    break;
                case MainMenuEntries.Settings:
                    _mainMenuHandler.ShowSettingsMenu();
                    break;
                case MainMenuEntries.Help:
                    _mainMenuHandler.ShowHelpMenu();
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
        var playerActionsHandler = new PlayerActionsHandler(_playerRepository, _maze, _gameView, mazeService);
        
        switch (actionType)
        {
            case TypeOfAction.Move:
                var direction = GameControlKeys.GetDirectionFromKey(key);
                playerActionsHandler.Move(direction);
                break;
            case TypeOfAction.Attack:
                _gameView.UpdateSpecialMessage(MessageType.Attack);
                var newMaze = mazeService.UpdateMaze(_maze);
                
                _gameView.UpdateMaze(newMaze);
                var keyPress = Console.ReadKey(true);
                var directionToAttack = GameControlKeys.GetDirectionFromKey(keyPress.Key);

                playerActionsHandler.Attack(directionToAttack);
                break;
            case TypeOfAction.Use:
                playerActionsHandler.UseInRoom(_playerRepository.Player!.Location);
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

            if (_maze[_playerRepository.Player!.Location].IsDangerous)
            {
                _maze[_playerRepository.Player.Location].GetObject<IDangerous>()!.Attack(_playerRepository.Player); 
            }
            
            var newMaze = mazeService.UpdateMaze(_maze);
            
            _gameView.UpdateMaze(newMaze);
        } while (true);
    }
}