using Model.Creatures;
using Model.Enums;
using Model.Extensions;
using Model.Objects;
using Model.Objects.Dangerous;

namespace Model.Factory;

public class MazeObjectFactory
{
    private readonly Player.Player? _player;
    
    public MazeObjectFactory(Player.Player player)
    {
        _player = player;
    }

    public Room.Room CreateRoom(int x, int y, params (ObjectType, int, int)[] objects)
    {
        var room = new Room.Room(x, y);

        foreach (var (objectType, objX, objY) in objects)
        {
            var obj = CreateObject(objectType, objX, objY);
            room.AddObject(obj);
        }

        return room;
    }

    private IPositionable CreateObject(ObjectType objectType, int x, int y) =>
        objectType switch
        {
            ObjectType.Fountain => new Fountain(x, y),
            ObjectType.Entrance => new Entrance(x, y),
            ObjectType.Amarok => new Amarok(x, y),
            ObjectType.Pit => new Pit(x, y),
            ObjectType.Maelstrom => new Maelstrom(x, y),
            ObjectType.Player => (_player ?? throw new InvalidOperationException("Player is not initialized"))
                .SetPosition(x, y),
            _ => throw new ArgumentException("Invalid object type.")
        };
}