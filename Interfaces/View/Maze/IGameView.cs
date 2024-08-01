using Interfaces.View.Menu;
using Shared.Enums.Views.Messages;
using Spectre.Console;

namespace Interfaces.View.Maze;

public interface IGameView : INonSelectableMenu
{
    string SpecialMessage { get; }
    Table? Maze { get; }
    void UpdateMaze(Table maze);
    void UpdateSpecialMessage(MessageType messageType);
}