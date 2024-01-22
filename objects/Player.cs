namespace TheFountainOfObjects;

public class Player : GameObject
{
    private byte _availableArrows = 5;
    internal override void SetName()
    {
        Console.Write("Please enter your name or press enter for default name: ");
        var name = Console.ReadLine();

        _name = string.IsNullOrEmpty(name) ? "Player" : name;
    }
    
    internal byte GetAvailableArrows()
    {
        return _availableArrows;
    }
    
    internal void DecreaseAvailableArrows()
    {
        _availableArrows--;
    }

    internal (int row, int col) Shoot(Direction direction, int fieldSize)
    {
        return MakeAnAction(direction, fieldSize, "shoot");
    }
}