using Interfaces.Models.Player;

namespace Interfaces.Models.Database;

public interface IDatabaseService
{
    List<IPlayerDTO> GetAllPlayers();
    IPlayerDTO? GetPlayerById(long playerId);
    IPlayerDTO? GetPlayerByName(string playerName);
    int AddPlayer(IPlayerDTO player);
    int UpdatePlayer(IPlayerDTO player);
}