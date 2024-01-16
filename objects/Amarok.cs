namespace TheFountainOfObjects;

public class Amarok : GameObject
{
    public Amarok((int row, int column) position)
    {
        Position = position;
    }

    internal void EatPlayer()
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine("You were eaten by the Amarok.");
        Console.WriteLine("Game over.");
        Console.WriteLine("Press any key to exit.");
        Console.ResetColor();
        Console.ReadKey(); 
    }
}