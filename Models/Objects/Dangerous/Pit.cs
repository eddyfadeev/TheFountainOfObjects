using Interfaces.Models.Objects;
using Interfaces.Models.Player;
using Shared;

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

    public void Attack(IPlayer player) => player.Kill();
}