using Model;
using Model.Enums;
using Model.Interfaces;
using Model.Maze;
using Spectre.Console;
using View.TableBuilder;

namespace Controller;

public class MazeService : IMazeService<IRoom>
{
    private IMaze<IRoom> _maze;
    private MazeSize _mazeSize;

    public MazeService(IMaze<IRoom> maze)
    {
        _mazeSize = MazeSize.Small;
        
        _maze = maze;
    }

    public void SetMazeSize(MazeSize mazeSize)
    {
        if (!IsMazeSizeCorrect(mazeSize))
        {
            AnsiConsole.WriteLine("Invalid maze size. Defaulting to small (4x4).");
            _mazeSize = MazeSize.Small;
        }
        _mazeSize = mazeSize;

        _maze = ResizeMaze(mazeSize);
    }
    
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
                dangerousOccupants.AddRange(_maze.MazeRooms[x, y].Occupants.OfType<IDangerous>());
            }
        }
        
        return dangerousOccupants;
    }
    
    public Table UpdateMaze()
    {
        var fieldSize = (int)_maze.MazeSize;
        
        var table = CreateInnerTable();
        AddColumns(table, fieldSize);
        AddRows(_maze, table);
        
        return table;
    }
    
    private bool IsMazeSizeCorrect(MazeSize value) => 
        value is 
            MazeSize.Small or
            MazeSize.Medium or
            MazeSize.Large;
    
    private IMaze<IRoom> ResizeMaze(MazeSize mazeSize) => new Maze(mazeSize);
}