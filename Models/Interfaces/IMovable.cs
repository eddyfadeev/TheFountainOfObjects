using Model.Enums;

namespace Model.Interfaces;

public interface IMovable
{
    bool CanMove(Direction direction);
    void Move(Direction direction);
}