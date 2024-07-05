using Model.Enums;

namespace Model.Maze;

public interface IMaze<T>
{
    MazeSize MazeSize { get; set; }
    T[,] MazeRooms { get; set; }
    T this[Location location] { get; set; }
}