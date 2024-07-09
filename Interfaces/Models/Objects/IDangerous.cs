using Interfaces.Models.Player;

namespace Interfaces.Models.Objects;

public interface IDangerous : IPositionable
{
    void Attack(IPlayer player);
}