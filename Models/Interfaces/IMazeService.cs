using Model.Enums;

namespace Model.Interfaces;

public interface IMazeService<T>
{
    T[,] MazeRooms { get; set; }
    MazeSize MazeSize { get; }
    T this[Location location] { get; set; }
    internal void SetMazeSize(MazeSize mazeSize);
}