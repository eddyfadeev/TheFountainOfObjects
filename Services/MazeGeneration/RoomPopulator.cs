using Interfaces.Models.Maze;
using Interfaces.Services;
using Interfaces.Services.Factories;
using Interfaces.Services.GameSettings;
using Model.Room;
using Shared;
using Shared.Enums.Models.Factory;

namespace Services.MazeGeneration;

public class RoomPopulator : IRoomPopulator
{
    private readonly IMazeService<IRoom> _mazeService;
    private readonly IMaze<IRoom> _maze;
    private readonly IMazeObjectFactory _mazeObjectFactory;
    private readonly IGameSettingsManager _gameSettingsManager;
    
    public RoomPopulator(
        IMaze<IRoom> maze, 
        IMazeService<IRoom> mazeService, 
        IMazeObjectFactory mazeObjectFactory, 
        IGameSettingsManager gameSettingsManager)
    {
        _maze = maze;
        _mazeService = mazeService;
        _mazeObjectFactory = mazeObjectFactory;
        _gameSettingsManager = gameSettingsManager;
    }
    
    public void InitializeRooms()
    {
        var mazeSize = (int)_maze.MazeSize;
        var mazeRooms = new IRoom[mazeSize, mazeSize];
        
        for (int y = 0; y < mazeSize; y++)
        {
            for (int x = 0; x < mazeSize; x++)
            {
                var location = new Location(x, y);
                mazeRooms[y, x] = new Room(location, _mazeService);
            }
        }
        
        _maze.MazeRooms = mazeRooms;
    }
    
    public void PopulateRooms()
    {
        var entranceLocation = GenerateRandomLocation(LocationType.Entrance);
        var fountainLocation = GenerateRandomLocation(LocationType.Fountain);
        
        AddEntranceAndPlayer(entranceLocation);
        AddFountain(fountainLocation);
        PopulateDangerousObjects();
    }
    
    private void AddObjectToRoom(Location location, ObjectType typeOfObject)
    {
        var objectToAdd = _mazeObjectFactory.CreateObject(typeOfObject, location);
        _maze[location].AddObject(objectToAdd);
    }
    
    private void PopulateDangerousObjects()
    {
        var allPositions = GetAllPositions();
        
        var dangerousObjects = GetDangerousObjects();
        
        var positionIndex = 0;

        foreach (var (objectType, count) in dangerousObjects)
        {
            for (int i = 0; i < count; i++)
            {
                var position = allPositions[positionIndex++];

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
        (ObjectType.Amarok, _gameSettingsManager.AmaroksCount),
        (ObjectType.Pit, _gameSettingsManager.PitsCount),
        (ObjectType.Maelstrom, _gameSettingsManager.MaelstromsCount)
    ];

    private Location GenerateRandomLocation(LocationType locationType)
    {
        const int rangeFactor = 2;
        var mazeSize = (int)_maze.MazeSize;
        var random = new Random();

        return locationType switch
        {
            LocationType.Entrance => new Location(random.Next(0, mazeSize / rangeFactor - 1), random.Next(0, mazeSize)),
            LocationType.Fountain => new Location(random.Next(mazeSize / rangeFactor + 1, mazeSize), random.Next(0, mazeSize)),
            _ => throw new ArgumentOutOfRangeException(nameof(locationType), locationType, null)
        };
    }
    
    private void AddEntranceAndPlayer(Location entranceLocation)
    {
        AddObjectToRoom(entranceLocation, ObjectType.Entrance);
        AddObjectToRoom(entranceLocation, ObjectType.Player);
    }
    
    private void AddFountain(Location fountainLocation)
    {
        AddObjectToRoom(fountainLocation, ObjectType.Fountain);
    }
}

public enum LocationType
{
    Entrance,
    Fountain
}