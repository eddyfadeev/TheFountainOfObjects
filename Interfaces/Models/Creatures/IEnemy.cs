using Interfaces.Models.Objects;

namespace Interfaces.Models.Creatures;

public interface IEnemy : IDangerous
{
    bool IsAlive { get; }
}