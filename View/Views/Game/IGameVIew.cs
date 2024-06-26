namespace View.Views.Game;

public interface IGameVIew
{
    public Table Maze { get; }
    public void UpdateMaze(Table maze);
}