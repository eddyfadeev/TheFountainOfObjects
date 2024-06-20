namespace Model.Creatures;

public class Amarok : IPositionable, IInteractable
{
    public Position Position { get; set; }
    
    public Amarok(int x, int y)
    {
        Position = new Position
        {
            X = x,
            Y = y
        };
    }

    public void Interact(Player.Player player) => throw new NotImplementedException();
}