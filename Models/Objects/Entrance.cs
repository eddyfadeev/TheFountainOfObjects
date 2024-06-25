namespace Model.Objects;

public class Entrance : IPositionable, IActivable
{
    public Location Location { get; set; }
    
    public Entrance(int x, int y)
    {
        Location = new Location
        {
            X = x,
            Y = y
        };
    }

    public static void Activate() => throw new NotImplementedException();
}