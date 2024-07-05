using Model.Enums;

namespace Model.Interfaces;

public interface IShootable
{
    void Attack(Direction direction);
}