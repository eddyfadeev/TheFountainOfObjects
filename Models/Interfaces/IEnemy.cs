namespace Model.Interfaces;

public interface IEnemy : IPositionable, IDangerous
{
    bool IsAlive { get; }
}