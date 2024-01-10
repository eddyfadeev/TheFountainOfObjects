namespace TheFountainOfObjects;

public abstract class GameObject
{
    private protected (int row, int column) Position { get; set; }
    
    public abstract void Move(Direction direction);

    public abstract void GetPosition();
}