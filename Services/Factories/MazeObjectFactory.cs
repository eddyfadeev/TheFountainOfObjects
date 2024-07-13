using Interfaces.Models.Maze;
using Interfaces.Models.Objects;
using Interfaces.Models.Repository;
using Interfaces.Services;
using Interfaces.Services.Factories;
using Model.Creatures;
using Model.Extensions;
using Model.Objects;
using Model.Objects.Dangerous;
using Shared;
using Shared.Enums.Models.Factory;

namespace Services.Factories;

public class MazeObjectFactory : IMazeObjectFactory
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IMazeService<IRoom> _mazeService;
    private readonly IMaze<IRoom> _maze;

    private readonly Dictionary<ObjectType, Func<Location, IPositionable>> _objectCreators; 
    public MazeObjectFactory(IMazeService<IRoom> mazeService, IMaze<IRoom> maze, IPlayerRepository playerRepository)
    {
        _mazeService = mazeService ?? throw new ArgumentNullException(nameof(mazeService));
        _maze = maze ?? throw new ArgumentNullException(nameof(maze));
        _playerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
        
        _objectCreators = new Dictionary<ObjectType, Func<Location, IPositionable>>
        {
            { ObjectType.Player, CreatePlayer },
            { ObjectType.Fountain, location => new Fountain(location) },
            { ObjectType.Entrance, location => new Entrance(location) },
            { ObjectType.Amarok, location => new Amarok(location) },
            { ObjectType.Pit, location => new Pit(location) },
            { ObjectType.Maelstrom, location => new Maelstrom(location, _mazeService, _maze) }
        };
    }
    public IPositionable CreateObject(ObjectType objectType, Location location)
    {
        if (_objectCreators.TryGetValue(objectType, out var creator))
        {
            return creator(location);
        }
        
        throw new ArgumentException("Invalid object type.");
    }
    
    private IPositionable CreatePlayer(Location location) => _playerRepository.Player!.SetPosition(location);
}