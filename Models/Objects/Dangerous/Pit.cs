namespace Model.Objects.Dangerous;

public class Pit : IPositionable, IDangerous
{
    public Location Location { get; set; }
    
    public Pit(int x, int y)
    {
        Location = new Location
        {
            X = x,
            Y = y
        };
    }

    public void Attack(Player.Player player) => throw new NotImplementedException();
}