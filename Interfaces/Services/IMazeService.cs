using Interfaces.Models.Maze;
using Interfaces.Models.Objects;
using Shared;
using Shared.Enums.Models.Objects.Maze;
using Spectre.Console;

namespace Interfaces.Services;

public interface IMazeService<T>
{
    List<IDangerous> GetAdjacentRoomsOccupants(Location location);
    Table UpdateMaze(IMaze<T> maze);
    public void ChangeMazeSize(MazeSize mazeSize);
    public bool IsWithinMazeBounds(Location location);
    public Location GetAdjustedLocation(Location location);
}