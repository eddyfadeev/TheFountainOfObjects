namespace Model.Objects.Dangerous;

public class Maelstrom : IPositionable, IInteractable
{
    public Location Location { get; set; }
    
    public Maelstrom(int x, int y)
    {
        Location = new Location
        {
            X = x,
            Y = y
        };
    }

    public void Interact(Player.Player player) => throw new NotImplementedException();
}