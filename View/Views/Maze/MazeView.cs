namespace View.Views.Maze;

public class MazeView : IMazeView
{
    public Table? Maze { get; }

    public void UpdateMaze(Table maze) => throw new NotImplementedException();
}

public interface IMazeView
{
    Table? Maze { get; }
    void UpdateMaze(Table maze);
}