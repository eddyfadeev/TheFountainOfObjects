namespace Model.Objects.Dangerous;

public class Pit : IDangerous
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

    public void Attack(Player.Player player) => player.Kill();
}