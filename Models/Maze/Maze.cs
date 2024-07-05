using Model.Enums;

namespace Model.Maze;

public class Maze : IMaze<IRoom>
{
    public MazeSize MazeSize { get; set; }
    public IRoom[,] MazeRooms { get; set; }

    public Maze(MazeSize mazeSize)
    {
        MazeSize = mazeSize;
        MazeRooms = new IRoom[(int)mazeSize, (int)mazeSize];
    }

    public Maze()
    {
        MazeSize = MazeSize.Small;
        MazeRooms = new IRoom[(int)MazeSize, (int)MazeSize];
    }

    public IRoom this[Location location]
    {
        get => MazeRooms[location.X, location.Y];
        set => MazeRooms[location.X, location.Y] = value;
    }
}