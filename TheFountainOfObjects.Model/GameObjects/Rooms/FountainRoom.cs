namespace TheFountainOfObjects.Model.GameObjects.Rooms;

public class FountainRoom((int row, int column) position, RoomType room = RoomType.Fountain)
    : RoomBase(position, room)
{
    private bool _isFountainActive;

    public override void IdentifyRoom()
    {
        Console.ForegroundColor = ConsoleColor;
        if (_isFountainActive)
        {
            Console.WriteLine("You hear the rushing waters from the Fountain of Objects.");
            Console.WriteLine("It has been reactivated!\n");
        }
        else
        {
            Console.WriteLine("You hear water dipping in this room.");
            Console.WriteLine("The Fountain of Objects is here!\n");
        }
        Console.ResetColor();

        ResetColor();
    }

    public bool ActivateFountain()
    {
        _isFountainActive = true;
        return true;
    }

    public bool IsFountainActive()
    {
        return _isFountainActive;
    }
}