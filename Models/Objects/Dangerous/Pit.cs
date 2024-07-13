using Interfaces.Models.Objects;
using Interfaces.Models.Player;
using Shared;

namespace Model.Objects.Dangerous;

public class Pit : IDangerous
{
    public Location Location { get; set; }
    
    public Pit(Location location)
    {
        Location = location;
    }

    public void Attack(IPlayer player) => player.Kill();
}