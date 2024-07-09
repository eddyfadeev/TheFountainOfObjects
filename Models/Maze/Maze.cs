using Interfaces.Models.Maze;
using Shared;
using Shared.Enums.Models.Objects.Maze;
using Spectre.Console;

namespace Model.Maze;

public class Maze : IMaze<IRoom>
{
    private MazeSize _mazeSize;
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
    public IRoom[,] MazeRooms { get; set; }

    public Maze()
    {
        MazeSize = MazeSize.Small;
        MazeRooms = new IRoom[(int)MazeSize, (int)MazeSize];
    }

    public IRoom this[Location location]
    {
        get => MazeRooms[location.Y, location.X];
        set => MazeRooms[location.Y, location.X] = value;
    }
    
    private bool IsMazeSizeCorrect(MazeSize value) => 
        value is 
            MazeSize.Small or
            MazeSize.Medium or
            MazeSize.Large;
    
    private void ResizeMaze() => MazeRooms = new IRoom[(int)_mazeSize, (int)_mazeSize];
}