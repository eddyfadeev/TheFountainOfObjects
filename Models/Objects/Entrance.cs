namespace Model.Objects;

public class Entrance : IPositionable, IActivable
{
    public Position Position { get; set; }
    
    public Entrance(int x, int y)
    {
        Position = new Position
        {
            X = x,
            Y = y
        };
    }

    public static void Activate() => throw new NotImplementedException();
}