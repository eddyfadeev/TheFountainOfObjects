using Model.Objects;
using Spectre.Console;

namespace Model.Room;

public class Room : IRoom
{
    private List<IPositionable> Occupants { get; }
    
    public bool IsVisited { get; private set; }
    public Location Location { get; set; }
    public Color RoomColor => SetRoomColor();
    public bool IsOccupied => Occupants.Count != 0;
    
    public Room(int x, int y)
    {
        Occupants = new List<IPositionable>();
        
        Location = new Location
        {
            X = x,
            Y = y
        };
        IsVisited = false;
    }

    public void AddObject(IPositionable obj) => Occupants.Add(obj);
    
    public T? GetObject<T>() where T : IPositionable => Occupants.OfType<T>().FirstOrDefault();
    
    public bool RemoveObject(IPositionable obj) => Occupants.Remove(obj);
    
    public bool IsOccupiedBy<T>() where T : IPositionable => Occupants.OfType<T>().Any();
    
    public void Visit() => IsVisited = true;
    
    private Color SetRoomColor() => 
        IsVisited switch
        {
            true when IsOccupiedBy<Player.Player>() => Color.Green,
            true when !IsOccupiedBy<Player.Player>() => Color.White,
            true when IsOccupiedBy<Entrance>() => Color.Yellow,
            true when IsOccupiedBy<Fountain>() => Color.Blue,
            _ => Color.Grey37 
        };
}