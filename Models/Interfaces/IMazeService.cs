using Model.Enums;
using Spectre.Console;

namespace Model.Interfaces;

public interface IMazeService<T>
{
    void SetMazeSize(MazeSize mazeSize);
    List<IDangerous> GetAdjacentRoomsOccupants(Location location);
    Table UpdateMaze();
}