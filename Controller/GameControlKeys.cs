using Model.Enums;

namespace Controller;

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