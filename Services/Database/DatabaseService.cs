using DataObjects.Player;
using Services.Database.Interfaces;

namespace Services.Database;

public class DatabaseService : IDatabaseService
{
    private readonly IPlayerRepository _playerRepository;
    
    public DatabaseService(IPlayerRepository playerRepository, IDatabaseInitializer initializer)
    {
        _playerRepository = playerRepository;
        initializer.InitializeDatabase();
    }

    public int AddPlayer(string name, int score)
    {
        var player = new PlayerDTO
        {
            Name = name,
            Score = score
        };
        
        return _playerRepository.AddPlayer(player);
    }

    public int UpdatePlayer(int playerId, string? name = null, int? score = null)
    {
        var player = _playerRepository.GetPlayerById(playerId);
        
        if (player is null)
        {
            throw new ArgumentException("Player not found.");
        }
        
        return _playerRepository.UpdatePlayer(
            player with
            {
                Name = name ?? player.Name, 
                Score = score ?? player.Score
            });
    }

    public PlayerDTO LoadPlayer(long playerId) => _playerRepository.GetPlayerById(playerId);

    public IEnumerable<PlayerDTO> RetrievePlayers() => _playerRepository.GetAllPlayers();
}