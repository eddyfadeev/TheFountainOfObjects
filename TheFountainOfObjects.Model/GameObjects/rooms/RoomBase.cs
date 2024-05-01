using Spectre.Console;

namespace TheFountainOfObjects.Model.GameObjects.rooms;

public abstract class RoomBase
{
    public (int row, int column) RoomLocation { get; init; }
    private readonly List<GameObject> _objectsInsideRoom = new ();
    public RoomType RoomType { get; }
    private bool _isEmpty = true;
    private bool _isRevealed;

    protected readonly ConsoleColor ConsoleColor;

    protected RoomBase((int row, int column) roomLocation, RoomType roomType)
    {
        RoomLocation = roomLocation;
        RoomType = roomType;
        
        ConsoleColor = roomType switch
        {
            RoomType.Entrance => ConsoleColor.Yellow,
            RoomType.Fountain => ConsoleColor.Blue,
            RoomType.Pit => ConsoleColor.Red,
            RoomType.Empty => ConsoleColor.White,
            _ => ConsoleColor.White
        };
    }

    public abstract void IdentifyRoom();
    
    public bool IsEmpty()
    {
        return _isEmpty;
    }

    public void SetEmpty()
    {
        _isEmpty = _objectsInsideRoom.Count == 0;
    }

    public void AddGameObject(GameObject gameObject)
    {
        _objectsInsideRoom.Add(gameObject);
        _isEmpty = false;
    }

    public void RemoveGameObject(GameObject gameObject)
    {
        _objectsInsideRoom.Remove(gameObject);
    }

    public bool ObjectIsPresent(Type type)
    {
        return _objectsInsideRoom.Exists(obj => obj.GetType() == type);
    }

    public void RevealRoom()
    {
        _isRevealed = true;
    }

    public bool IsRevealed()
    {
        return _isRevealed;
    }
    
    protected void ResetColor()
    {
        Console.ResetColor();
    }

    public GameObject? GetObject(Type type)
    {
        var gameObject = _objectsInsideRoom.Find(obj => obj.GetType() == type);
        
        return gameObject;
    }
}