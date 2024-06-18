namespace Model.GameObjects.Rooms;

public abstract class RoomBase
{
    public (int row, int column) RoomCoordinates { get; init; }
    private readonly List<GameObject> _objectsInsideRoom = new ();
    public RoomType RoomType { get; }
    private bool _isEmpty = true;
    private bool _isRoomRevealed;

    protected readonly ConsoleColor ConsoleColor;

    protected RoomBase((int row, int column) roomCoordinates, RoomType roomType)
    {
        RoomCoordinates = roomCoordinates;
        RoomType = roomType;
        
        // TODO: Reconsider usage with a new layout structure mentioned in Program.cs file
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
    
    public bool IsRoomEmpty()
    {
        return _isEmpty;
    }

    public void SetRoomEmpty()
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
        _isRoomRevealed = true;
    }

    public bool IsRoomRevealed()
    {
        return _isRoomRevealed;
    }
    
    // TODO: Reconsider usage with a new layout structure mentioned in Program.cs file
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