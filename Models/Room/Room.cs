namespace Model.Room;

public class Room : IPositionable
{
    public Position Position { get; set; }

    private List<IPositionable> Objects { get; set; }
    
    public Room(int x, int y)
    {
        Position.X = x;
        Position.Y = y;
        Objects = new List<IPositionable>();
    }
    
    public void AddObject(IPositionable obj)
    {
        Objects.Add(obj);
    }
    
    public T? GetObject<T>() where T : IPositionable => Objects.OfType<T>().FirstOrDefault();
}