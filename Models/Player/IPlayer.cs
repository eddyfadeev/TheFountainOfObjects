namespace Model.Player;

public interface IPlayer : IPositionable
{
    public long? Id { get; set; }
    public int? Score { get; set; }
    public string? Name { get; set; }
    public int Arrows { get; } 
    public void Shoot();
}