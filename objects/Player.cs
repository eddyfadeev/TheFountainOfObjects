namespace TheFountainOfObjects;

public class Player : GameObject
{
    

    public override void Move(Direction direction)
    { 
        Position = direction switch
        {
            Direction.East => (Position.row, Position.column + 1),
            Direction.West => (Position.row, Position.column - 1),
            Direction.North => (Position.row - 1, Position.column),
            Direction.South => (Position.row + 1, Position.column),
            _ => Position
        };
    }
    
    public override void GetPosition()
    {
        Console.WriteLine($"You are in the room at row: {Position.row + 1}, column: {Position.column + 1}");
    }
}