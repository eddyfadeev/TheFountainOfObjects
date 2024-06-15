using DataObjects.Player;

namespace Services.Database.Interfaces;

public interface IDatabaseService
{
    int AddPlayer(string name, int score);
    int UpdatePlayer(int playerId, string? name = null, int? score = null);
    PlayerDTO LoadPlayer(long playerId);
    IEnumerable<PlayerDTO> RetrievePlayers();
}