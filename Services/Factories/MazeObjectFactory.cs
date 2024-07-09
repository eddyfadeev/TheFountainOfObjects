using Interfaces.Models.Database;
using Interfaces.Models.Factory;
using Interfaces.Models.Maze;
using Interfaces.Models.Objects;
using Interfaces.Services;
using Model.Creatures;
using Model.Extensions;
using Model.Objects;
using Model.Objects.Dangerous;
using Shared;
using Shared.Enums.Models.Factory;

namespace Services.Factories;

public class MazeObjectFactory : IMazeObjectFactory
{
    private readonly IMazeService<IRoom> _mazeService;
    private readonly IMaze<IRoom> _maze;
    private readonly IPlayerRepository _playerRepository;
    
    public MazeObjectFactory(IMazeService<IRoom> mazeService, IMaze<IRoom> maze, IPlayerRepository playerRepository)
    {
        _mazeService = mazeService;
        _maze = maze;
        _playerRepository = playerRepository;
    }
    public IPositionable CreateObject(ObjectType objectType, Location location) =>
        objectType switch
        {
            ObjectType.Player => _playerRepository.Player!.SetPosition(location),
            ObjectType.Fountain => new Fountain(location.X, location.Y),
            ObjectType.Entrance => new Entrance(location.X, location.Y),
            ObjectType.Amarok => new Amarok(location.X, location.Y),
            ObjectType.Pit => new Pit(location.X, location.Y),
            ObjectType.Maelstrom => new Maelstrom(location.X, location.Y, _mazeService, _maze),
            _ => throw new ArgumentException("Invalid object type.")
        };
}