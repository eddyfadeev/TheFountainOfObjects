namespace TheFountainOfObjects.Model.GameObjects.rooms;

public class EmptyRoom((int row, int col) position, RoomType room = RoomType.Empty) 
    : RoomBase(position, room)
{
    public override void IdentifyRoom()
    {
        Console.ForegroundColor = ConsoleColor;
        Console.WriteLine("You don't feel anything else in this room.\n");
        ResetColor();
    }
}