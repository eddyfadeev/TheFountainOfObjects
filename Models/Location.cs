namespace Model;

public class Location
{
    public int X { get; set; }
    public int Y { get; set; }
    
    public Location(int x, int y)
    {
        X = x;
        Y = y;
    }
    
    public Location() { }
    
    public static bool operator ==(Location a, Location b) =>
        a?.X == b?.X && a?.Y == b?.Y;
    
    public static bool operator !=(Location a, Location b) =>
        !(a == b);
    
    public bool IsEqual(Location other) =>
        X == other.X && Y == other.Y;
    
    public override int GetHashCode() =>
        (X, Y).GetHashCode();
}