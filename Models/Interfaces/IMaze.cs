using Model.Enums;

namespace Model.Interfaces;

public interface IMaze<T>
{
    public T[,] MazeRooms { get; set; }
    public MazeSize MazeSize { get; }
    public T this[Location location] { get; set; }
    internal void SetMazeSize(MazeSize mazeSize);
}