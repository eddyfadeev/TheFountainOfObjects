using Spectre.Console;

namespace Model.Interfaces;

public interface IRoom : IPositionable
{
    bool IsVisited { get; }
    bool IsOccupied { get; }
    Color RoomColor { get; }
    void AddObject(IPositionable obj);
    T? GetObject<T>() where T : IPositionable;
    bool RemoveObject(IPositionable obj);
    bool IsOccupiedBy<T>() where T : IPositionable;
    void Visit();
}