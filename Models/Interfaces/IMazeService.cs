using Model.Enums;
using Model.Maze;
using Spectre.Console;

namespace Model.Interfaces;

public interface IMazeService<T>
{
    List<IDangerous> GetAdjacentRoomsOccupants(Location location);
    Table UpdateMaze(IMaze<IRoom> maze);
    public void ChangeMazeSize(MazeSize mazeSize);
    public bool IsWithinMazeBounds(Location location);
    public Location GetAdjustedLocation(Location location);
}