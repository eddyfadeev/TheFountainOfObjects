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
using static Services.Utilities.Utilities;

namespace Services.Factories;

public class MazeObjectFactory : IMazeObjectFactory
{
    private readonly IPlayerRepository _playerRepository;

    private readonly Dictionary<ObjectType, Func<Location, IPositionable>> _objectCreators; 
    public MazeObjectFactory(IMazeService<IRoom> mazeService, IMaze<IRoom> maze, IPlayerRepository playerRepository)
    {
        CheckNulls(mazeService, maze, playerRepository);
        
        _playerRepository = playerRepository;
        
        _objectCreators = new Dictionary<ObjectType, Func<Location, IPositionable>>
        {
            { ObjectType.Player, CreatePlayer },
            { ObjectType.Fountain, location => new Fountain(location) },
            { ObjectType.Entrance, location => new Entrance(location) },
            { ObjectType.Amarok, location => new Amarok(location) },
            { ObjectType.Pit, location => new Pit(location) },
            { ObjectType.Maelstrom, location => new Maelstrom(location, mazeService, maze) }
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