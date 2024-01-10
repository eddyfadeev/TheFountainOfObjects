namespace TheFountainOfObjects;

public class EmptyRoom(int row, int col, RoomType room = RoomType.Empty) 
    : Room(row, col, room)
{
    public override RoomType IdentifyRoom()
    {
        Console.WriteLine("You feel nothing in this room.");
        Console.WriteLine(!IsEmpty() ? "" : "It is empty.");

        return RoomType;
    }
}