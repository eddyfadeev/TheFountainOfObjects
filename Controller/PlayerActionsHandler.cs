using Model;
using Model.Enums;
using Model.Interfaces;
using Model.Player;
using Services.Database.Interfaces;
using View.Views.Game;

namespace Controller;

public class PlayerActionsHandler : IMovable, IShootable
{
    private readonly IMazeService<IRoom> _mazeService;
    private readonly IPlayer _player;
    private readonly IGameView _gameView;
    
    public PlayerActionsHandler(IPlayerRepository playerRepository, IMazeService<IRoom> mazeService, IGameView gameView)
    {
        _player = playerRepository.Player!;
        _mazeService = mazeService;
        _gameView = gameView;
    }

    // TODO: Properly implement the method
    public bool Attack(Direction direction)
    {
        if (CanAttack(direction))
        {
            var targetLocation = GetTargetLocation(_player.Location, direction);
            var targetRoom = _mazeService[targetLocation];

            var enemy = targetRoom.GetObject<IEnemy>();

            if (enemy is not null)
            {
                targetRoom.RemoveObject(enemy);
                return true;
            }
        }

        return false;
    }
    
    public void Move(Direction direction)
    {
        if (CanMove(direction))
        {
            var newLocation = GetTargetLocation(_player.Location, direction);
            var currentRoom = _mazeService[_player.Location];
            var newRoom = _mazeService[newLocation];
            
            currentRoom.RemoveObject(_player);
            newRoom.AddObject(_player);
            _player.Location = newLocation;
            
        }
    }

    private bool CanAttack(Direction direction)
    {
        var targetLocation = GetTargetLocation(_player.Location, direction);
        var targetRoom = _mazeService[targetLocation];
        
        return targetRoom.IsOccupiedBy<IEnemy>();
    }

    private bool CanMove(Direction direction)
    {
        var newLocation = GetTargetLocation(_player.Location, direction);
        
        return IsWithinMazeBounds(newLocation);
    }
    
    private Location GetTargetLocation(Location currentLocation, Direction direction) => direction switch
    {
        Direction.North => new Location(currentLocation.X - 1, currentLocation.Y),
        Direction.East => new Location(currentLocation.X, currentLocation.Y + 1),
        Direction.South => new Location(currentLocation.X + 1, currentLocation.Y),
        Direction.West => new Location(currentLocation.X, currentLocation.Y - 1),
        _ => currentLocation
    };

    private bool IsWithinMazeBounds(Location location) =>
        location.X >= 0 && location.X < (int)_mazeService.MazeSize &&
        location.Y >= 0 && location.Y < (int)_mazeService.MazeSize;
}

internal static class GameControlKeys
{
    public static List<ConsoleKey> DirectionKeys { get; } =
    [
        ConsoleKey.W,
        ConsoleKey.A,
        ConsoleKey.S,
        ConsoleKey.D,
        ConsoleKey.UpArrow,
        ConsoleKey.LeftArrow,
        ConsoleKey.DownArrow,
        ConsoleKey.RightArrow
    ];

    public static List<ConsoleKey> InteractionKeys { get; } =
    [
        ConsoleKey.E,
        ConsoleKey.Enter
    ];
    
    public static ConsoleKey AttackTrigger => ConsoleKey.Spacebar;
    public static ConsoleKey PauseKey => ConsoleKey.Escape;

    public static Dictionary<Enum, object> GameKeys = new()
    {
        {TypeOfAction.Attack, AttackTrigger},
        {TypeOfAction.Move, DirectionKeys},
        {TypeOfAction.Interact, InteractionKeys},
        {TypeOfAction.Pause, PauseKey}
    };
    
    public static List<ConsoleKey> GetAllKeys()
    {
        var allKeys = DirectionKeys.Concat(InteractionKeys).ToList();
        allKeys.Add(AttackTrigger);
        allKeys.Add(PauseKey);
        
        return allKeys;
    }
    
    public static TypeOfAction GetTypeOfAction(ConsoleKey key)
    {
        foreach (var keyType in GameKeys)
        {
            if (keyType.Value is List<ConsoleKey> keyList && keyList.Contains(key) ||
                keyType.Value is ConsoleKey singleKey && singleKey == key)
            {
                return (TypeOfAction)keyType.Key;
            }
        }
        
        throw new ArgumentException("Invalid key.");
    }
    
    public static Direction GetDirectionFromKey(ConsoleKey key) =>
        key switch
        {
            ConsoleKey.W or ConsoleKey.UpArrow => Direction.North,
            ConsoleKey.D or ConsoleKey.RightArrow => Direction.East,
            ConsoleKey.S or ConsoleKey.DownArrow => Direction.South,
            ConsoleKey.A or ConsoleKey.LeftArrow => Direction.West,
            _ => throw new ArgumentException("Invalid key.")
        };
    
    public static bool IsValidKey(ConsoleKey key) => GetAllKeys().Contains(key);
}

public enum TypeOfAction 
{
    Move,
    Attack,
    Interact,
    Pause,
    DoNothing
}