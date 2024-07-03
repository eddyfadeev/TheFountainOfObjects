namespace Model.Objects;

public class Entrance : IPositionable
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
}