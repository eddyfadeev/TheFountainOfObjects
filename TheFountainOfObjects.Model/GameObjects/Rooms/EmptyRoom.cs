namespace TheFountainOfObjects.Model.GameObjects.Rooms;

public class EmptyRoom((int row, int col) position, RoomType roomType = RoomType.Empty)
    : RoomBase(position, roomType)
{
    public override void IdentifyRoom()
    {
        Console.ForegroundColor = ConsoleColor;
        Console.WriteLine("You don't feel anything else in this room.\n");
        ResetColor();
    }
}