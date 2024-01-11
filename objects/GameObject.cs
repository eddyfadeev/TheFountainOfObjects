namespace TheFountainOfObjects;

public abstract class GameObject
{
    protected (int row, int column) Position { get; set; }
    private protected string? _name;
    
    public abstract (int row, int column) Move(Direction direction, int fieldSize);
    
    public abstract bool CanMove(Direction direction, int fieldSize);

    public abstract (int row, int column) GetPosition();
    
    public string GetName()
    {
        return _name;
    }

    internal virtual void SetName()
    {
        _name = this.ToString();
    }
}