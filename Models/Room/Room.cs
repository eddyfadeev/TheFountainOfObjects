using Model.Interfaces;

namespace Model.Room;

public class Room : IPositionable
{
    public int X { get; set; }
    public int Y { get; set; }

    private List<IPositionable> Objects { get; set; }
    
    public Room(int x, int y)
    {
        X = x;
        Y = y;
        Objects = new List<IPositionable>();
    }
    
    public void AddObject(IPositionable obj)
    {
        Objects.Add(obj);
    }
    
    public T? GetObject<T>() where T : IPositionable => Objects.OfType<T>().FirstOrDefault();
}