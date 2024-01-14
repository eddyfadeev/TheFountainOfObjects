namespace TheFountainOfObjects.intefaces;

public interface IMovable
{
    public (int row, int column) Move(Direction direction, int fieldSize);
    
    public bool CanMove(Direction direction, int fieldSize);
}