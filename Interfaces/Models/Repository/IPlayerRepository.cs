using Interfaces.Models.Player;

namespace Interfaces.Models.Repository;

public interface IPlayerRepository
{
    IPlayer? Player { get; set; }
    int AddPlayer(string name, int score);
    int UpdatePlayer(int playerId, string? name = null, int? score = null);
    IPlayerDTO LoadPlayer(long playerId);
    IPlayerDTO LoadPlayer(string playerName);
    List<IPlayerDTO>? GetAllPlayers();
}