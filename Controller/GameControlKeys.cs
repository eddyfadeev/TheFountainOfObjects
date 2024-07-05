using Model.Enums;

namespace Controller;

internal static class GameControlKeys
{
    private static List<ConsoleKey> DirectionKeys { get; } =
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

    private static List<ConsoleKey> InteractionKeys { get; } =
    [
        ConsoleKey.E,
        ConsoleKey.Enter
    ];
    
    private static ConsoleKey AttackTrigger => ConsoleKey.Spacebar;
    private static ConsoleKey PauseKey => ConsoleKey.Escape;

    private static readonly Dictionary<Enum, object> GameKeys = new()
    {
        {TypeOfAction.Attack, AttackTrigger},
        {TypeOfAction.Move, DirectionKeys},
        {TypeOfAction.Interact, InteractionKeys},
        {TypeOfAction.Pause, PauseKey}
    };
    
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
        
        return TypeOfAction.DoNothing;
    }
    
    public static Direction GetDirectionFromKey(ConsoleKey key) =>
        key switch
        {
            ConsoleKey.W or ConsoleKey.UpArrow => Direction.North,
            ConsoleKey.D or ConsoleKey.RightArrow => Direction.East,
            ConsoleKey.S or ConsoleKey.DownArrow => Direction.South,
            ConsoleKey.A or ConsoleKey.LeftArrow => Direction.West,
            _ => throw new ArgumentException("Invalid direction key.")
        };
}