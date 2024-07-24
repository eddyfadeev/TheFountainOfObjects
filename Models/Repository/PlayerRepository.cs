using Interfaces.Models.Database;
using Interfaces.Models.Player;
using Interfaces.Models.Repository;
using Model.Player;

namespace Model.Repository;

public class PlayerRepository(IDatabaseService databaseService) : IPlayerRepository
{
    public IPlayer? Player { get; set; }
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
            throw new ArgumentException("Player not found. Update failed.");
        }
        
        var playerToUpdate = new PlayerDTO
        {
            Id = playerId, 
            Name = name ?? player.Name, 
            Score = score ?? player.Score
        };
        
        return databaseService.UpdatePlayer(playerToUpdate);
    }

    public IPlayerDTO LoadPlayer(long playerId) => 
        databaseService.GetPlayerById(playerId) ?? 
        throw new ArgumentException("Player not found. Load by ID failed.");
    
    public IPlayerDTO LoadPlayer(string playerName) => 
        databaseService.GetPlayerByName(playerName) ?? 
        throw new ArgumentException("Player not found. Load by name failed.");

    public List<IPlayerDTO>? GetAllPlayers() => 
        databaseService.GetAllPlayers();
}