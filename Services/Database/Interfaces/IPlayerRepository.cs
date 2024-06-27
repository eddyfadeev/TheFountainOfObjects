using Model.Player;

namespace Services.Database.Interfaces;

public interface IPlayerRepository
{
    Player? Player { get; set; }
    int AddPlayer(string name, int score);
    int UpdatePlayer(int playerId, string? name = null, int? score = null);
    PlayerDTO LoadPlayer(long playerId);
    PlayerDTO LoadPlayer(string playerName);
    List<PlayerDTO> GetAllPlayers();
}