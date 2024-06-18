using Model.Creatures;
using Model.Enums;
using Model.Extensions;
using Model.Interfaces;
using Model.Objects;
using Model.Objects.Dangerous;

namespace Model.Factory;

public static class MazeObjectFactory
{
    private static Player.Player? Player { get; set; }

    public static Room.Room CreateRoom(int x, int y, params (ObjectType, int, int)[] objects)
    {
        var room = new Room.Room(x, y);

        foreach (var (objectType, objX, objY) in objects)
        {
            var obj = CreateObject(objectType, objX, objY);
            room.AddObject(obj);
        }

        return room;
    }
    public static IPositionable CreateObject(ObjectType objectType, int x, int y)
    {
        return objectType switch
        {
            ObjectType.Fountain => new Fountain { X = x, Y = y },
            ObjectType.Entrance => new Entrance { X = x, Y = y },
            ObjectType.Amarok => new Amarok { X = x, Y = y },
            ObjectType.Pit => new Pit { X = x, Y = y },
            ObjectType.Maelstrom => new Maelstrom { X = x, Y = y },
            ObjectType.Player => (Player ?? throw new InvalidOperationException("Player is not initialized")).SetPosition(x, y),
            _ => throw new ArgumentException("Invalid object type.")
        };
    }
}