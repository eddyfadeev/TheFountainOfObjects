namespace Interfaces.Controller;

public interface IPlayerActionsHandler : IMove, IShoot
{
    public event Action? OnPlayerActionCompleted;
}