using Shared.Enums.Models;

namespace Interfaces.Controller;

public interface IShoot
{
    void Attack(Direction direction);
}