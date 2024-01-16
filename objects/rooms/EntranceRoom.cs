using System.Drawing;

namespace TheFountainOfObjects;

public class EntranceRoom((int row, int column) position, RoomType room = RoomType.Entrance)
    : Room(position, room)
{
    internal override void IdentifyRoom()
    {
        Console.ForegroundColor = _consoleColor;
        Console.WriteLine("You see light in this room coming from outside the cavern." );
        Console.WriteLine("This is the entrance.\n");
        ResetColor();
    }
}