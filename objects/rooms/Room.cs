namespace TheFountainOfObjects;

public abstract class Room(int row, int column, RoomType roomType)
{
    public (int row, int column) RoomLocation { get; init; } = (row, column);
    private List<GameObject> insideRoom = new ();
    internal RoomType RoomType { get; } = roomType;
    private bool _isEmpty = true;
    private bool _isRevealed;

    public abstract RoomType IdentifyRoom();
    
    public bool IsEmpty()
    {
        return _isEmpty;
    }
    
    public void SetEmpty()
    {
        _isEmpty = insideRoom.Count == 0;
    }
    
    internal void AddGameObject(GameObject gameObject)
    {
        insideRoom.Add(gameObject);
        _isEmpty = false;
    }
    
    internal void RemoveGameObject(GameObject gameObject)
    {
        insideRoom.Remove(gameObject);
    }
    
    internal bool ObjectIsPresent(Type type)
    {
        return insideRoom.Any(obj => obj.GetType() == type);
    }
    
    internal void RevealRoom()
    {
        _isRevealed = true;
    }
    
    internal bool IsRevealed()
    {
        return _isRevealed;
    }
}