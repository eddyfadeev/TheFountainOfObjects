namespace Model;

public sealed record Location
{
    public int X { get; init; }
    public int Y { get; init; }
    
    public Location(int x, int y)
    {
        X = x;
        Y = y;
    }
    
    public Location() { }

    public override string ToString()
    {
        var x = X + 1;
        var y = Y + 1;
        return $"X: {x}, Y: {y}";
    }
}