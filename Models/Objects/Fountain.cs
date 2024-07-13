using Interfaces.Models.Objects;
using Shared;

namespace Model.Objects;

public class Fountain : IActivable
{
    public Location Location { get; set; }
    public bool IsActivated { get; private set; }

    public Fountain(Location location)
    {
        Location = location;
        
        IsActivated = false;
    }

    public void Activate()
    {
        IsActivated = true;
    }
}