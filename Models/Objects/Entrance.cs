namespace Model.Objects;

public class Entrance : IEntrance
{
    public Location Location { get; set; }
    public void Exit(Player.Player player) => throw new NotImplementedException();

    public Entrance(int x, int y)
    {
        Location = new Location
        {
            X = x,
            Y = y
        };
    }
}

public interface IEntrance : IPositionable
{
    void Exit(Player.Player player);
}