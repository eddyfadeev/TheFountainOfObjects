using Interfaces.Models.Factory;
using Interfaces.Models.GameSettings;
using Interfaces.Models.Maze;
using Interfaces.Services;
using Model.Room;
using Shared;
using Shared.Enums.Models.Factory;

namespace Services.MazeGeneration;

public class RoomPopulator : IRoomPopulator
{
    private readonly IMazeService<IRoom> _mazeService;
    private readonly IMaze<IRoom> _maze;
    private readonly IMazeObjectFactory _mazeObjectFactory;
    private readonly IGameSettingsRepository _gameSettingsRepository;
    
    public RoomPopulator(
        IMaze<IRoom> maze, 
        IMazeService<IRoom> mazeService, 
        IMazeObjectFactory mazeObjectFactory, 
        IGameSettingsRepository gameSettingsRepository)
    {
        _maze = maze;
        _mazeService = mazeService;
        _mazeObjectFactory = mazeObjectFactory;
        _gameSettingsRepository = gameSettingsRepository;
    }
    
    public void GenerateRooms()
    {
        var mazeSize = (int)_maze.MazeSize;
        var newMaze = new IRoom[mazeSize, mazeSize];
        
        for (int y = 0; y < mazeSize; y++)
        {
            for (int x = 0; x < mazeSize; x++)
            {
                var location = new Location(x, y);
                newMaze[y, x] = new Room(location, _mazeService);
            }
        }
        
        _maze.MazeRooms = newMaze;
    }
    
    public void SetRoomOccupants()
    {
        var random = new Random();
        var mazeSize = (int)_maze.MazeSize;

        var entranceLocation = new Location(random.Next(0, mazeSize / 2 - 1), random.Next(0, mazeSize));
        var fountainLocation = new Location(random.Next(mazeSize / 2 + 1, mazeSize), random.Next(0, mazeSize));
        
        AddObjectToRoom(entranceLocation, ObjectType.Entrance);
        AddObjectToRoom(entranceLocation, ObjectType.Player);
        AddObjectToRoom(fountainLocation, ObjectType.Fountain);
        AddDangerousObjects();
    }
    
    private void AddObjectToRoom(Location location, ObjectType typeOfObject)
    {
        var objectToAdd = _mazeObjectFactory.CreateObject(typeOfObject, location);
        _maze[location].AddObject(objectToAdd);
    }
    
    private void AddDangerousObjects()
    {
        var allPositions = GetAllPositions();
        
        var dangerousObjects = GetDangerousObjects();
        
        int currentIndex = 0;

        foreach (var (objectType, count) in dangerousObjects)
        {
            for (int i = 0; i < count; i++)
            {
                var position = allPositions[currentIndex++];

                if (!_maze[position].IsOccupied)
                {
                    AddObjectToRoom(position, objectType);
                }
                else
                {
                    i--;
                }
            }
        }
    }
    
    private List<Location> GetAllPositions()
    {
        var random = new Random();
        var mazeSize = (int)_maze.MazeSize;
        
        return Enumerable.Range(0, mazeSize)
            .SelectMany(x => Enumerable.Range(0, mazeSize), (x, y) => new Location(x, y))
            .OrderBy(_ => random.Next())
            .ToList();
    }

    private List<(ObjectType, int)> GetDangerousObjects() =>
    [
        (ObjectType.Amarok, _gameSettingsRepository.AmaroksCount),
        (ObjectType.Pit, _gameSettingsRepository.PitsCount),
        (ObjectType.Maelstrom, _gameSettingsRepository.MaelstromsCount)
    ];
}