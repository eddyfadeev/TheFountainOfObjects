namespace View.Views.GameView;

public interface IGameVIew
{
    public Table Maze { get; }
    public void UpdateMaze(Table maze);
}