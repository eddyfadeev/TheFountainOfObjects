namespace Model.GameObjects.Rooms;

public class EntranceRoom((int row, int column) position, RoomType room = RoomType.Entrance)
    : RoomBase(position, room)
{
    public override void IdentifyRoom()
    {
        Console.ForegroundColor = ConsoleColor;
        Console.WriteLine("You see light in this room coming from outside the cavern." );
        Console.WriteLine("This is the entrance.\n");
        ResetColor();
    }
}