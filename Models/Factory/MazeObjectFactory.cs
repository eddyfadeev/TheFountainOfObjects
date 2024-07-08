using Model.Creatures;
using Model.Enums;
using Model.Maze;
using Model.Objects;
using Model.Objects.Dangerous;

namespace Model.Factory;

public class MazeObjectFactory : IMazeObjectFactory
{
    private readonly IMazeService<IRoom> _mazeService;
    private readonly IMaze<IRoom> _maze;
    public MazeObjectFactory(IMazeService<IRoom> mazeService, IMaze<IRoom> maze)
    {
        _mazeService = mazeService;
        _maze = maze;
    }
    public IPositionable CreateObject(ObjectType objectType, Location location) =>
        objectType switch
        {
            ObjectType.Fountain => new Fountain(location.X, location.Y),
            ObjectType.Entrance => new Entrance(location.X, location.Y),
            ObjectType.Amarok => new Amarok(location.X, location.Y),
            ObjectType.Pit => new Pit(location.X, location.Y),
            ObjectType.Maelstrom => new Maelstrom(location.X, location.Y, _mazeService, _maze),
            _ => throw new ArgumentException("Invalid object type.")
        };
}