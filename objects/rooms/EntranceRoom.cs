using System.Drawing;

namespace TheFountainOfObjects;

public class EntranceRoom(int row = 0, int column = 0, RoomType room = RoomType.Entrance)
    : Room(row, column, room)
{
    internal override void IdentifyRoom()
    {
        Console.ForegroundColor = _consoleColor;
        Console.WriteLine("You see light in this room coming from outside the cavern." );
        Console.WriteLine("This is the entrance.");
        ResetColor();
    }
}