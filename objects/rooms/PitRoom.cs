namespace TheFountainOfObjects;

public class PitRoom((int row, int column) position, RoomType roomType = RoomType.Pit) : Room(position, roomType)
{
    internal void FallInPit()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("You fell in a pit and died.");
        Console.WriteLine("Game over.");
        Console.WriteLine("Press any key to exit.");
        Console.ResetColor();
        Console.ReadKey();
    }

    internal override void IdentifyRoom()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("You feel a draft. There is a pit in a nearby room.\n");
        Console.ResetColor();
    }
}