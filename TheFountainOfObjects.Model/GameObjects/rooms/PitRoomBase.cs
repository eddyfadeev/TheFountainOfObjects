namespace TheFountainOfObjects.Model.GameObjects.rooms;

public class PitRoomBase((int row, int column) position, RoomType roomType = RoomType.Pit) : RoomBase(position, roomType)
{
    public void FallInPit()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("You fell in a pit and died.");
        Console.WriteLine("Game over.");
        Console.WriteLine("Press any key to exit.");
        Console.ResetColor();
        Console.ReadKey();
    }

    public override void IdentifyRoom()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("You feel a draft. There is a pit in a nearby room.\n");
        Console.ResetColor();
    }
}