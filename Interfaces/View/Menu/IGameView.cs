using Shared.Enums.Models.Messages;
using Spectre.Console;

namespace Interfaces.View.Menu;

public interface IGameView : INonSelectableMenu
{
    string SpecialMessage { get; }
    Table? Maze { get; }
    void UpdateMaze(Table maze);
    void UpdateSpecialMessage(MessageType messageType);
}