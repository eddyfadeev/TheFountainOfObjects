namespace Model.Interfaces;

public interface IEnemy : IDangerous
{
    bool IsAlive { get; }
}