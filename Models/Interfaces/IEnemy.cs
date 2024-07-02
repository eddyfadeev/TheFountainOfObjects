namespace Model.Interfaces;

public interface IEnemy : IPositionable, IInteractable
{
    void Attack(Player.Player player);
}