using Model;
using Model.Enums;
using Model.Interfaces;
using Model.Maze;
using Spectre.Console;

namespace Controller;

public class MazeService : IMazeService<IRoom>
{
    private readonly IMaze<IRoom> _maze;
    
    public MazeService(IMaze<IRoom> maze)
    {
        _maze = maze;
    }
    
    public List<IDangerous> GetAdjacentRoomsOccupants(Location location)
    {
        var mazeSize = (int)_maze.MazeSize;
        
        var maxX = location.X + 1 < mazeSize ? location.X + 1 : location.X;
        var minX = location.X - 1 >= 0 ? location.X - 1 : location.X;
        var maxY = location.Y + 1 < mazeSize ? location.Y + 1 : location.Y;
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
    
    public Table UpdateMaze(IMaze<IRoom> maze)
    {
        var fieldSize = (int)maze.MazeSize;
        
        var table = CreateInnerTable();
        AddColumns(table, fieldSize);
        AddRows(maze, table);
        
        return table;
    }
    
    public void ChangeMazeSize(MazeSize mazeSize) => _maze.MazeSize = mazeSize;
}