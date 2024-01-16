namespace TheFountainOfObjects;

public abstract class Room((int row, int column) position, RoomType roomType)
{
    public (int row, int column) RoomLocation { get; init; } = position;
    private List<GameObject> _insideRoom = new ();
    internal RoomType RoomType { get; } = roomType;
    private bool _isEmpty = true;
    private bool _isRevealed;

    protected ConsoleColor _consoleColor = roomType switch
    {
        RoomType.Entrance => ConsoleColor.Yellow,
        RoomType.Fountain => ConsoleColor.Blue,
        RoomType.Pit => ConsoleColor.Red,
        RoomType.Empty => ConsoleColor.White,
        _ => ConsoleColor.White
    };

    internal abstract void IdentifyRoom();
    
    internal bool IsEmpty()
    {
        return _isEmpty;
    }
    
    internal void SetEmpty()
    {
        _isEmpty = _insideRoom.Count == 0;
    }
    
    internal void AddGameObject(GameObject gameObject)
    {
        _insideRoom.Add(gameObject);
        _isEmpty = false;
    }
    
    internal void RemoveGameObject(GameObject gameObject)
    {
        _insideRoom.Remove(gameObject);
    }
    
    internal bool ObjectIsPresent(Type type)
    {
        return _insideRoom.Any(obj => obj.GetType() == type);
    }
    
    internal void RevealRoom()
    {
        _isRevealed = true;
    }
    
    internal bool IsRevealed()
    {
        return _isRevealed;
    }
    
    protected void ResetColor()
    {
        Console.ResetColor();
    }

    internal GameObject GetObject(Type type)
    {
        var gameObject = _insideRoom.FirstOrDefault(obj => obj.GetType() == type);
        
        return gameObject;
    }
}