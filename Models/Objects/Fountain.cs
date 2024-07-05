namespace Model.Objects;

public class Fountain : IActivable
{
    public Location Location { get; set; }
    public bool IsActivated { get; private set; }

    public Fountain(int x, int y)
    {
        Location = new Location
        {
            X = x,
            Y = y
        };
        IsActivated = false;
    }

    public void Activate()
    {
        IsActivated = true;
    }
}