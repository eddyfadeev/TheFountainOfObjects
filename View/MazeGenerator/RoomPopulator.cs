using Model;
using Model.Enums;
using Model.GameSettings;
using Model.Interfaces;
using Model.Maze;
using Model.Room;

namespace View.MazeGenerator;

public class RoomPopulator : IRoomPopulator
{
    public void GenerateRooms(IMaze<IRoom> maze, IMazeService<IRoom> mazeService)
    {
        var mazeSize = (int)maze.MazeSize;
        var newMaze = new IRoom[mazeSize, mazeSize];
        
        for (int y = 0; y < mazeSize; y++)
        {
            for (int x = 0; x < mazeSize; x++)
            {
                var location = new Location(x, y);
                newMaze[y, x] = new Room(location, mazeService);
            }
        }
        
        maze.MazeRooms = newMaze;
    }
    
    public void SetRoomOccupants(
        IMaze<IRoom> maze, 
        IPlayerRepository playerRepository, 
        IMazeObjectFactory mazeObjectFactory, 
        IGameSettingsRepository gameSettingsRepository)
    {
        var random = new Random();
        var mazeSize = (int)maze.MazeSize;

        var entranceLocation = new Location(random.Next(0, mazeSize / 2 - 1), random.Next(0, mazeSize));
        var fountainLocation = new Location(random.Next(mazeSize / 2 + 1, mazeSize), random.Next(0, mazeSize));
        playerRepository.Player.Location = entranceLocation;
        
        AddObjectToRoom(entranceLocation, maze.MazeRooms, mazeObjectFactory.CreateObject(ObjectType.Entrance, entranceLocation));
        AddObjectToRoom(entranceLocation, maze.MazeRooms, playerRepository.Player);
        AddObjectToRoom(fountainLocation, maze.MazeRooms, mazeObjectFactory.CreateObject(ObjectType.Fountain, fountainLocation));
        AddDangerousObjects(maze.MazeRooms, gameSettingsRepository, mazeObjectFactory);
    }
    
    private void AddObjectToRoom(Location location, IRoom[,] maze, IPositionable obj)
    {
        maze[location.Y, location.X].AddObject(obj);
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

                if (!maze[position.Y, position.X].IsOccupied)
                {
                    var objectToPlace = mazeObjectFactory.CreateObject(objectType, position);
                    AddObjectToRoom(position, maze, objectToPlace);
                }
                else
                {
                    i--;
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