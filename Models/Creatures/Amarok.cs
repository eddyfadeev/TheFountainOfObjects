namespace Model.Creatures;

public class Amarok : IEnemy
{
    public Location Location { get; set; }
    public bool IsAlive { get; }
    
    public Amarok(int x, int y)
    {
        Location = new Location
        {
            X = x,
            Y = y
        };
        
        IsAlive = true;
    }

    public void Attack(Player.Player player) => player.Kill();
}