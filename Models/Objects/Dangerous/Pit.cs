namespace Model.Objects.Dangerous;

public class Pit : IPositionable, IInteractable
{
    public Position Position { get; set; }
    
    public Pit(int x, int y)
    {
        Position = new Position
        {
            X = x,
            Y = y
        };
    }

    public void Interact(Player.Player player) => throw new NotImplementedException();
}