using Interfaces.Controller;
using Interfaces.Models.Database;
using Interfaces.Models.Maze;
using Interfaces.Models.Objects;
using Interfaces.Models.Player;
using Interfaces.Models.Repository;
using Interfaces.Services;
using Interfaces.Services.GameSettings;
using Interfaces.View.Factory;
using Interfaces.View.Menu;
using Model.Objects;
using Shared;
using Shared.Enums.Models.Messages;
using Shared.Enums.Views.Menus;

namespace Controller;

public class GameController
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IPlayerRepository _playerRepository;
    private readonly MainMenuHandler _mainMenuHandler;
    private readonly IMaze<IRoom> _maze;
    private readonly IGameView _gameView;
    
    public GameController(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        var menuCommandFactory = _serviceProvider.GetRequiredService<IMenuCommandFactory>();
        _playerRepository = _serviceProvider.GetRequiredService<IPlayerRepository>();
        var gameSettingsRepository = _serviceProvider.GetRequiredService<IGameSettingsManager>();
        _mainMenuHandler = new MainMenuHandler(menuCommandFactory, _playerRepository, gameSettingsRepository);
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
        var playerActionsHandler = _serviceProvider.GetRequiredService<IPlayerActionsHandler>();
        var actionType = GameControlKeys.GetTypeOfAction(key);
        
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
        var playerActionsHandler = _serviceProvider.GetRequiredService<IPlayerActionsHandler>();
        _playerRepository.Player!.Revive();
        _playerRepository.Player!.ResetArrows();

        playerActionsHandler.OnPlayerActionCompleted += OnPlayerActionCompleted;
        
        
        _gameView.Display();
        do
        {
            var pressedKey = Console.ReadKey(true);
            ProcessKeyPress(pressedKey.Key);
            
            CheckForDanger(_playerRepository.Player!);

            if (!_playerRepository.Player.IsAlive)
            {
                // TODO: Game over screen
                break;
            }

            if (CheckIfPlayerWon(_playerRepository.Player!.Location))
            {
                // TODO: Winning screen and save score
                break;
            }
            
            var newMaze = mazeService.UpdateMaze(_maze);
            
            _gameView.UpdateMaze(newMaze);
        } while (true);
    }
    
    private void Attack(IPlayer player, IDangerous dangerous) => dangerous.Attack(player);

    private void CheckForDanger(IPlayer player)
    {
        var location = player.Location;

        if (!_maze[location].IsDangerous)
        {
            return;
        }

        var dangerousObject = _maze[location].GetObject<IDangerous>();
            
        Attack(player, dangerousObject);
    }

    private bool CheckIfPlayerWon(Location location)
    {
        var isAtEntrance = _maze[location].IsEntrance;
        var fountainIsActivated = _maze.MazeRooms.OfType<IRoom>().Any(room =>
        {
            var activableObject = room.GetObject<IActivable>();
            return activableObject is not null && activableObject.IsActivated;
        });

        if (!isAtEntrance || !fountainIsActivated)
        {
            return false;
        }

        _gameView.UpdateSpecialMessage(MessageType.Victory);
        return true;
    }
    
    private void OnPlayerActionCompleted()
    {
        CheckForDanger(_playerRepository.Player!);
    }
}