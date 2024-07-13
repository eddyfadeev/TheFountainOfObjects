using Interfaces.Models.Creatures;
using Interfaces.Models.Player;
using Shared;

namespace Model.Creatures;

public class Amarok : IEnemy
{
    public Location Location { get; set; }
    public bool IsAlive { get; }
    
    public Amarok(Location location)
    {
        Location = location;
        
        IsAlive = true;
    }

    public void Attack(IPlayer player) => player.Kill();
}