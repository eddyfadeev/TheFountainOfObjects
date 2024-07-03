namespace Model;

public sealed class Location : IEquatable<Location>
{
    public int X { get; init; }
    public int Y { get; init; }
    
    public Location(int x, int y)
    {
        X = x;
        Y = y;
    }
    
    public Location() { }
    
    public static bool operator ==(Location a, Location b) =>
        a.X == b.X && a.Y == b.Y;
    
    public static bool operator !=(Location a, Location b) =>
        !(a == b);
    
    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public bool Equals(Location? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != this.GetType())
        {
            return false;
        }

        return Equals((Location)obj);
    }
}