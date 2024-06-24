using Model.Creatures;
using Model.Enums;
using Model.Objects;
using Model.Objects.Dangerous;

namespace Model.Factory;

public class MazeObjectFactory
{
    public IPositionable CreateEntrance(Location location) =>
        CreateObject(ObjectType.Entrance, location);
    
    public IPositionable CreateFountain(Location location) =>
        CreateObject(ObjectType.Fountain, location);
    
    public IPositionable CreateAmarok(Location location) =>
        CreateObject(ObjectType.Amarok, location);
    
    public IPositionable CreatePit(Location location) =>
        CreateObject(ObjectType.Pit, location);
    
    public IPositionable CreateMaelstrom(Location location) =>
        CreateObject(ObjectType.Maelstrom, location);

    private IPositionable CreateObject(ObjectType objectType, Location location) =>
        objectType switch
        {
            ObjectType.Fountain => new Fountain(location.X, location.Y),
            ObjectType.Entrance => new Entrance(location.X, location.Y),
            ObjectType.Amarok => new Amarok(location.X, location.Y),
            ObjectType.Pit => new Pit(location.X, location.Y),
            ObjectType.Maelstrom => new Maelstrom(location.X, location.Y),
            _ => throw new ArgumentException("Invalid object type.")
        };
}