using Model.Player;

namespace Services.Database.Interfaces;

public interface IDatabaseService
{
    List<PlayerDTO> GetAllPlayers();
    PlayerDTO? GetPlayerById(long playerId);
    int AddPlayer(PlayerDTO player);
    int UpdatePlayer(PlayerDTO player);
}