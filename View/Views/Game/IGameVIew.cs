namespace View.Views.Game;

public interface IGameVIew : INonSelectableMenu
{
    public Table Maze { get; }
    public void UpdateMaze(Table maze);
}