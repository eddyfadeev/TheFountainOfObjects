using Interfaces.Models.Objects;

namespace Interfaces.Models.Player;

public interface IPlayer : IPositionable
{
    public long? Id { get; set; }
    public int? Score { get; set; }
    public string? Name { get; set; }
    public int Arrows { get; } 
    public bool IsAlive { get; }
    public void Shoot();
    public void Kill();
    public void Revive();
    public void ResetArrows();
}