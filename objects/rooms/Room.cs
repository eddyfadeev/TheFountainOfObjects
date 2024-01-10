namespace TheFountainOfObjects;

public abstract class Room(int row, int column, RoomType roomType)
{
    public (int row, int column) RoomLocation { get; init; } = (row, column);
    private List<GameObject> insideRoom = new ();
    internal RoomType RoomType { get; } = roomType;
    private bool _isEmpty = true;

    public abstract RoomType IdentifyRoom();
    
    public bool IsEmpty()
    {
        return _isEmpty;
    }
    
    public void SetEmpty()
    {
        if (insideRoom.Count == 0)
        {
            _isEmpty = true;
        }
        else
        {
            Console.WriteLine("This room is not empty.");
        }
    }
    
    public void SetOccupancy(GameObject gameObject)
    {
        _isEmpty = false;
        insideRoom.Add(gameObject);
    }
}