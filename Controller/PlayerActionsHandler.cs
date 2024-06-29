using Model;
using Model.Enums;
using Model.Interfaces;
using Model.Player;

namespace Controller;

public class PlayerActionsHandler : IMovable, IShootable
{
    private readonly IMaze<IRoom> _maze;
    private readonly IPlayer _player;
    
    public PlayerActionsHandler(IPlayer player, IMaze<IRoom> maze)
    {
        _player = player;
        _maze = maze;
    }

    public bool Attack(Direction direction)
    {
        if (CanAttack(direction))
        {
            var targetLocation = GetTargetLocation(_player.Location, direction);
            var targetRoom = _maze[targetLocation];
            
            
        }
    }
    
    public bool CanAttack(Direction direction) => throw new NotImplementedException();
    
    public void Move(Direction direction) => throw new NotImplementedException();
    
    public bool CanMove(Direction direction) => throw new NotImplementedException();
    
    public void ProcessKeyPress(ConsoleKey key)
    {
        
        if (IsValidKey(key))
        {
            
        }
    }
    
    private bool IsValidKey(ConsoleKey key) => GameControlKeys.GetAllKeys().Contains(key);

    private static Direction? GetDirectionFromKey(ConsoleKey key) =>
        key switch
        {
            ConsoleKey.W or ConsoleKey.UpArrow => Direction.North,
            ConsoleKey.D or ConsoleKey.RightArrow => Direction.East,
            ConsoleKey.S or ConsoleKey.DownArrow => Direction.South,
            ConsoleKey.A or ConsoleKey.LeftArrow => Direction.West,
            _ => null
        };
    
    
    // TODO: nearby room? 
    private Location GetTargetLocation(Location currentLocation, Direction direction) => direction switch
    {
        Direction.North => new Location(currentLocation.X, currentLocation.Y - 1),
        Direction.East => new Location(currentLocation.X + 1, currentLocation.Y),
        Direction.South => new Location(currentLocation.X, currentLocation.Y + 1),
        Direction.West => new Location(currentLocation.X - 1, currentLocation.Y),
        _ => currentLocation
    };

    private bool IsWithinMazeBounds(Location location) =>
        location.X >= 0 && location.X < (int)_maze.MazeSize &&
        location.Y >= 0 && location.Y < (int)_maze.MazeSize;
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

    public static List<ConsoleKey> GetAllKeys()
    {
        var allKeys = DirectionKeys.Concat(InteractionKeys).ToList();
        allKeys.Add(AttackTrigger);
        allKeys.Add(PauseKey);
        
        return allKeys;
    }
    
    public static string GetTypeOfAction(ConsoleKey key)
    {
        foreach (var keyType in GameKeys)
        {
            if (keyType.Value is List<ConsoleKey> keyList && keyList.Contains(key) ||
                keyType.Value is ConsoleKey singleKey && singleKey == key)
            {
                return keyType.Key;
            }
        }
        
        return string.Empty;
    }
    
    public static Dictionary<string, object> GameKeys = new()
    {
        {"attack", AttackTrigger},
        {"move", DirectionKeys},
        {"interaction", InteractionKeys},
        {"pause", PauseKey}
    };
}
