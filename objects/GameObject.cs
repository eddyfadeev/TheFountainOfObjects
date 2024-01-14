namespace TheFountainOfObjects;

public abstract class GameObject
{
    protected (int row, int column) Position { get; set; }
    private protected string? _name;

    public (int row, int column) GetPosition()
    {
        return (Position.row, Position.column);
    }
    
    internal virtual void SetStartPosition(int row, int column)
    {
        Position = (row, column);
    }
    
    public string GetName()
    {
        return _name;
    }

    internal virtual void SetName()
    {
        _name = this.ToString();
    }
    
    
}