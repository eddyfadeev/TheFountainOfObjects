using Model.Messages.Enum;
using Spectre.Console;

namespace Model.Interfaces;

public interface IRoom : IPositionable
{
    internal List<IPositionable> Occupants { get; }
    bool IsVisited { get; }
    bool IsOccupied { get; }
    Color RoomColor { get; }
    List<MessageType> Messages { get; }
    void AddObject(IPositionable obj);
    T? GetObject<T>() where T : IPositionable;
    bool RemoveObject(IPositionable obj);
    bool IsOccupiedBy<T>() where T : IPositionable;
    void Visit();
}