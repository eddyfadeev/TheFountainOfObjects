using Spectre.Console;

namespace Interfaces.View.Maze;

public interface IMazeView
{
    Table? Maze { get; }
    void UpdateMaze(Table maze);
}