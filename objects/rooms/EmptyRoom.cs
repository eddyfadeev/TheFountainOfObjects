namespace TheFountainOfObjects;

public class EmptyRoom((int row, int col) position, RoomType room = RoomType.Empty) 
    : Room(position, room)
{
    internal override void IdentifyRoom()
    {
        Console.ForegroundColor = _consoleColor;
        Console.WriteLine("You don't feel anything else in this room.\n");
        ResetColor();
    }
}