namespace Model.Objects;

public class Fountain : IPositionable, IActivable
{
    public Position Position { get; set; }

    public Fountain(int x, int y)
    {
        Position = new Position
        {
            X = x,
            Y = y
        };
    }

    public static void Activate() => throw new NotImplementedException();
}