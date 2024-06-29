using Model.Enums;

namespace Model.Interfaces;

public interface IShootable
{
    bool CanAttack(Direction direction);
    bool Attack(Direction direction);
}