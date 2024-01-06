namespace TheFountainOfObjects;

public abstract class Room(int row, int column, RoomType roomType)
{
    public (int row, int column) RoomLocation { get; init; } = (row, column);
    protected RoomType RoomType { get; } = roomType;
    protected bool isEmpty = true;

    public abstract RoomType IdentifyRoom();
    
    public bool IsEmpty()
    {
        return isEmpty;
    }
}