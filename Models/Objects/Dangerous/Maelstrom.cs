namespace Model.Objects.Dangerous;

public class Maelstrom : IPositionable, IInteractable
{
    public Position Position { get; set; }
    
    public Maelstrom(int x, int y)
    {
        Position = new Position
        {
            X = x,
            Y = y
        };
    }

    public void Interact(Player.Player player) => throw new NotImplementedException();
}