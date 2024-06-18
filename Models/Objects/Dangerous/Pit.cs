using Model.Interfaces;

namespace Model.Objects.Dangerous;

public class Pit : IPositionable, IInteractable
{
    public int X { get; set; }

    public int Y { get; set; }

    public void Interact(Player.Player player) => throw new NotImplementedException();
}