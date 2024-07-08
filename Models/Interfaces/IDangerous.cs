namespace Model.Interfaces;

public interface IDangerous : IPositionable
{
    void Attack(Player.Player player);
}