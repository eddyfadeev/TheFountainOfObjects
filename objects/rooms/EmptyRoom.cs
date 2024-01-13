namespace TheFountainOfObjects;

public class EmptyRoom(int row, int col, RoomType room = RoomType.Empty) 
    : Room(row, col, room)
{
    internal override void IdentifyRoom()
    {
        Console.ForegroundColor = _consoleColor;
        Console.WriteLine("You feel nothing in this room.");
        ResetColor();
    }
}