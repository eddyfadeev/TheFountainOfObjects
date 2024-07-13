using Interfaces.Models.Objects;
using Shared;

namespace Model.Objects;

public class Entrance : IEntrance
{
    public Location Location { get; set; }

    public Entrance(Location location)
    {
        Location = location;
    }
}

public interface IEntrance : IPositionable;