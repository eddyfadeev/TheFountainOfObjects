namespace View.Views.Game;

public interface IGameView : INonSelectableMenu
{
    public Table? Maze { get; }
    public void UpdateMaze(Table maze);
}