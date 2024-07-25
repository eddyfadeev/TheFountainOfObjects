using Interfaces.Models.Objects;
using Shared.Enums.Views.Messages;
using Spectre.Console;

namespace Interfaces.Models.Maze;

public interface IRoom : IPositionable
{
    public List<IPositionable> Occupants { get; }
    bool IsVisited { get; }
    bool IsOccupied { get; }
    bool IsDangerous { get; }
    bool IsEntrance { get; }
    Color RoomColor { get; }
    List<MessageType> Messages { get; }
    void AddObject(IPositionable obj);
    T? GetObject<T>() where T : IPositionable;
    bool RemoveObject(IPositionable obj);
    bool IsOccupiedBy<T>() where T : IPositionable;
    void Visit();
}