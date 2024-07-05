using Model.Messages.Enum;

namespace View.Views.Game;

public interface IGameView : INonSelectableMenu
{
    string SpecialMessage { get; }
    Table? Maze { get; }
    void UpdateMaze(Table maze);
    void UpdateSpecialMessage(MessageType messageType);
}