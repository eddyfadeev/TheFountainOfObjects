using TheFountainOfObjects.Model.Enums;

namespace TheFountainOfObjects.Model.GameObjects;

public class Player : GameObject
{
    private byte _availableArrows = 5;
    public override void SetName()
    {
        Console.Write("Please enter your name or press enter for default name: ");
        var name = Console.ReadLine();

        _name = string.IsNullOrEmpty(name) ? "Player" : name;
    }

    public byte GetAvailableArrows()
    {
        return _availableArrows;
    }

    public void DecreaseAvailableArrows()
    {
        _availableArrows--;
    }

    public (int row, int col) Shoot(Direction direction, int fieldSize)
    {
        return MakeAnAction(direction, fieldSize, "shoot");
    }
}