namespace Model.Creatures;

public class Amarok : IEnemy
{
    private bool _isAlive;
    public Location Location { get; set; }
    public bool IsAlive => _isAlive;
    
    public Amarok(int x, int y)
    {
        Location = new Location
        {
            X = x,
            Y = y
        };
        _isAlive = true;
    }

    public void Attack(Player.Player player) => throw new NotImplementedException();
}