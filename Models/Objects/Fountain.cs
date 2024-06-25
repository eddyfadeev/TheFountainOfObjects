namespace Model.Objects;

public class Fountain : IPositionable, IActivable
{
    public Location Location { get; set; }

    public Fountain(int x, int y)
    {
        Location = new Location
        {
            X = x,
            Y = y
        };
    }

    public static void Activate() => throw new NotImplementedException();
}