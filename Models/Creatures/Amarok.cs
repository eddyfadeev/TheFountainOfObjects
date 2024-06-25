namespace Model.Creatures;

public class Amarok : IPositionable, IInteractable
{
    public Location Location { get; set; }
    
    public Amarok(int x, int y)
    {
        Location = new Location
        {
            X = x,
            Y = y
        };
    }


    public void Interact(Player.Player player) => throw new NotImplementedException();
}