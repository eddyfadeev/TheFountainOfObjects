using Interfaces.Models.Maze;
using Interfaces.Models.Objects;
using Interfaces.Services;
using Shared;
using Shared.Enums.Models.Objects.Maze;
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

        for (int y = minY; y <= maxX; y++)
        {
            for (int x = minX; x <= maxY; x++)
            {
                if (x == location.X && y == location.Y)
                {
                    continue;
                }
                
                dangerousOccupants.AddRange(_maze.MazeRooms[y, x].Occupants.OfType<IDangerous>());
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
    
    public bool IsWithinMazeBounds(Location location) =>
        XIsWithinMazeBounds(location.X) &&
        YIsWithinMazeBounds(location.Y);

    public Location GetAdjustedLocation(Location location)
    {
        var newX = location.X;
        var newY = location.Y;
        
        if (IsWithinMazeBounds(location))
        {
            return location;
        }

        if (!XIsWithinMazeBounds(location.X))
        {
            newX = AdjustCoordinate(location.X);
        }

        if (!YIsWithinMazeBounds(location.Y))
        {
            newY = AdjustCoordinate(location.Y);
        }
        
        return new Location
        {
            X = newX,
            Y = newY
        };
    }

    private int AdjustCoordinate(int coordinate)
    {
        int newCoordinate;
        
        if (coordinate < 0)
        {
            newCoordinate = 0;
        } 
        else if (coordinate >= (int)_maze.MazeSize - 1)
        {
            newCoordinate = (int) _maze.MazeSize - 1;
        }
        else
        {
            newCoordinate = coordinate;
        }

        return newCoordinate;
    }
    
    private bool XIsWithinMazeBounds(int x) => x >= 0 && x < (int)_maze.MazeSize;
    
    private bool YIsWithinMazeBounds(int y) => y >= 0 && y < (int)_maze.MazeSize;
}