using Model.Enums;
using Spectre.Console;

namespace Model.Maze;

public class MazeService : IMazeService<IRoom>
{
    private MazeSize _mazeSize;
    
    public IRoom[,] MazeRooms { get; set; }

    public MazeSize MazeSize
    {
        get => _mazeSize;
        set
        {
            if (!IsMazeSizeCorrect(value))
            {
                AnsiConsole.WriteLine("Invalid maze size. Defaulting to small (4x4).");
                value = MazeSize.Small;
            }
            
            _mazeSize = value;
            ResizeMaze();
        }
    }

    public IRoom this[Location location]
    {
        get => MazeRooms[location.X, location.Y];
        set => MazeRooms[location.X, location.Y] = value;
    }

    public MazeService()
    {
        _mazeSize = MazeSize.Small;
        
        MazeRooms = new IRoom[(int)_mazeSize, (int)_mazeSize];
    }

    public void SetMazeSize(MazeSize mazeSize)
    {
        MazeSize = mazeSize;
    }
    
    private bool IsMazeSizeCorrect(MazeSize value) => 
        value is 
            MazeSize.Small or
            MazeSize.Medium or
            MazeSize.Large;
    
    public List<IDangerous> GetAdjacentRoomsOccupants(Location location)
    {
        var maxX = location.X + 1 < (int)_mazeSize ? location.X + 1 : location.X;
        var minX = location.X - 1 >= 0 ? location.X - 1 : location.X;
        var maxY = location.Y + 1 < (int)_mazeSize ? location.Y + 1 : location.Y;
        var minY = location.Y - 1 >= 0 ? location.Y - 1 : location.Y;
        
        var dangerousOccupants = new List<IDangerous>();

        for (int x = minX; x <= maxX; x++)
        {
            for (int y = minY; y <= maxY; y++)
            {
                if (x == location.X && y == location.Y)
                {
                    continue;
                }
                dangerousOccupants.AddRange(MazeRooms[x, y].Occupants.OfType<IDangerous>());
            }
        }
        
        return dangerousOccupants;
    }
    
    private void ResizeMaze() => MazeRooms = new IRoom[(int)_mazeSize, (int)_mazeSize];
}