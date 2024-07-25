using Shared.Enums.Controller;
using Shared.Enums.Models;

namespace Controller;

internal static class GameControlKeys
{
    private static readonly HashSet<ConsoleKey> DirectionKeys =
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

    private static readonly HashSet<ConsoleKey> InteractionKeys =
    [
        ConsoleKey.E,
        ConsoleKey.Enter
    ];
    
    private static readonly ConsoleKey AttackTrigger = ConsoleKey.Spacebar;
    private static readonly ConsoleKey PauseKey = ConsoleKey.Escape;

    private static readonly Dictionary<TypeOfAction, object> GameKeys = new()
    {
        { TypeOfAction.Attack, AttackTrigger },
        { TypeOfAction.Move, DirectionKeys },
        { TypeOfAction.Use, InteractionKeys },
        { TypeOfAction.Pause, PauseKey }
    };
    
    public static TypeOfAction GetTypeOfAction(ConsoleKey key)
    {
        foreach (var (action, keyObj) in GameKeys)
        {
            switch (keyObj)
            {
                case HashSet<ConsoleKey> keySet when keySet.Contains(key):
                    return action;
                case ConsoleKey singleKey when singleKey == key:
                    return action;
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