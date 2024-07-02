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
    
    private static bool IsMazeSizeCorrect(MazeSize value) => 
        value is 
            MazeSize.Small or
            MazeSize.Medium or
            MazeSize.Large;
    
    private void ResizeMaze() => MazeRooms = new IRoom[(int)_mazeSize, (int)_mazeSize];
}