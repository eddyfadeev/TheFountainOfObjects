using Model.Player;
using Services.Database.Interfaces;

namespace Services.Database.Repository;

public class PlayerRepository(IDatabaseService databaseService) : IPlayerRepository
{
    public int AddPlayer(string name, int score)
    {
        var player = new PlayerDTO
        {
            Name = name,
            Score = score
        };
        
        return databaseService.AddPlayer(player);
    }

    public int UpdatePlayer(int playerId, string? name = null, int? score = null)
    {
        var player = databaseService.GetPlayerById(playerId);
        
        if (player is null)
        {
            throw new ArgumentException("Player not found.");
        }
        
        return databaseService.UpdatePlayer(
            player with
            {
                Name = name ?? player.Name, 
                Score = score ?? player.Score
            });
    }

    public PlayerDTO LoadPlayer(long playerId) => 
        databaseService.GetPlayerById(playerId) ?? 
        throw new ArgumentException("Player not found.");

    public List<PlayerDTO> GetAllPlayers() => 
        databaseService.GetAllPlayers();
}