using Shared;
using Shared.Enums.Models.Objects.Maze;

namespace Interfaces.Models.Maze;

public interface IMaze<T>
{
    MazeSize MazeSize { get; set; }
    T[,] MazeRooms { get; set; }
    T this[Location location] { get; set; }
}