using Model.Interfaces;

namespace Model.Creatures;

public class Amarok : IPositionable, IInteractable
{
    public int X { get; set; }

    public int Y { get; set; }

    public void Interact(Player.Player player) => throw new NotImplementedException();
}