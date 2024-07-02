using Model;
using Model.Enums;
using Model.GameSettings;
using Model.Interfaces;
using Model.Room;

namespace View.MazeGenerator;

public class RoomPopulator : IRoomPopulator
{
    public void GenerateRooms(IMazeService<IRoom> mazeService)
    {
        var mazeSize = (int)mazeService.MazeSize;
        var maze = new IRoom[mazeSize, mazeSize];
        
        for (int i = 0; i < mazeSize; i++)
        {
            for (int j = 0; j < mazeSize; j++)
            {
                maze[i, j] = new Room(i, j);
            }
        }
        
        mazeService.MazeRooms = maze;
    }
    
    public void SetRoomOccupants(
        IMazeService<IRoom> mazeService, 
        IPlayerRepository playerRepository, 
        IMazeObjectFactory mazeObjectFactory, 
        IGameSettingsRepository gameSettingsRepository)
    {
        var random = new Random();
        var mazeSize = (int)mazeService.MazeSize;
        var maze = mazeService.MazeRooms;

        var entranceLocation = new Location(random.Next(0, mazeSize), random.Next(0, mazeSize / 2 - 1));
        var fountainLocation = new Location(random.Next(0, mazeSize), random.Next(mazeSize / 2 + 1, mazeSize));
        playerRepository.Player.Location = entranceLocation;
        
        AddObjectToRoom(entranceLocation, maze, mazeObjectFactory.CreateObject(ObjectType.Entrance, entranceLocation));
        AddObjectToRoom(entranceLocation, maze, playerRepository.Player);
        AddObjectToRoom(fountainLocation, maze, mazeObjectFactory.CreateObject(ObjectType.Fountain, fountainLocation));
        AddDangerousObjects(maze, gameSettingsRepository, mazeObjectFactory);
    }
    
    private void AddObjectToRoom(Location location, IRoom[,] maze, IPositionable obj)
    {
        maze[location.X, location.Y].AddObject(obj);
    }
    
    private void AddDangerousObjects(
        IRoom[,] maze, 
        IGameSettingsRepository gameSettingsRepository, 
        IMazeObjectFactory mazeObjectFactory)
    {
        var allPositions = GetAllPositions(maze.GetLength(0));
        
        var dangerousObjects = GetDangerousObjects(gameSettingsRepository);
        
        int currentIndex = 0;

        foreach (var (objectType, count) in dangerousObjects)
        {
            for (int i = 0; i < count; i++)
            {
                var position = allPositions[currentIndex++];

                if (!maze[position.X, position.Y].IsOccupied)
                {
                    var objectToPlace = mazeObjectFactory.CreateObject(objectType, position);
                    AddObjectToRoom(position, maze, objectToPlace);
                }
            }
        }
    }
    
    private List<Location> GetAllPositions(int mazeSize)
    {
        var random = new Random();
        
        return Enumerable.Range(0, mazeSize)
            .SelectMany(x => Enumerable.Range(0, mazeSize), (x, y) => new Location(x, y))
            .OrderBy(_ => random.Next())
            .ToList();
    }

    private List<(ObjectType, int)> GetDangerousObjects(IGameSettingsRepository gameSettingsRepository) =>
    [
        (ObjectType.Amarok, gameSettingsRepository.AmaroksCount),
        (ObjectType.Pit, gameSettingsRepository.PitsCount),
        (ObjectType.Maelstrom, gameSettingsRepository.MaelstromsCount)
    ];
}