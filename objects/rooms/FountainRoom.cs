namespace TheFountainOfObjects;

public class FountainRoom((int row, int column) position, RoomType room = RoomType.Fountain)
    : Room(position, room)
{
    private bool _isFountainActive;
    
    internal override void IdentifyRoom()
    {
        Console.ForegroundColor = _consoleColor;
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
    
    internal bool ActivateFountain()
    {
        _isFountainActive = true;
        return true;
    }
    
    internal bool IsFountainActive()
    {
        return _isFountainActive;
    }
}