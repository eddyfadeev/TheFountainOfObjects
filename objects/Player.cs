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

    public override bool CanMove(Direction direction, int fieldSize)
    {
        return direction switch
        {
            Direction.East => Position.column + 1 < fieldSize,
            Direction.West => Position.column - 1 >= 0,
            Direction.North => Position.row - 1 >= 0,
            Direction.South => Position.row + 1 < fieldSize,
            _ => false
        };
    }

    // TODO: Rename to GetPositionMessage and implement GetPosition
    public override (int row, int column) GetPosition()
    {
        //Console.WriteLine($"You are in the room at row: {Position.row + 1}, column: {Position.column + 1}");
        return (Position.row, Position.column);
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