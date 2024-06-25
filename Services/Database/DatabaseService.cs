using Dapper;
using Model.Player;
using Services.Database.Helpers;
using Services.Database.Interfaces;

namespace Services.Database;

public class DatabaseService : IDatabaseService
{
    private readonly IConnectionProvider _connectionProvider;
    
    public DatabaseService(IConnectionProvider connectionProvider, IDatabaseInitializer initializer)
    {
        _connectionProvider = connectionProvider;
        initializer.InitializeDatabase();
    }
    
    public List<PlayerDTO> GetAllPlayers()
    {
        using var connection = _connectionProvider.GetConnection();
        const string query = "SELECT * FROM Players ORDER BY Score DESC";
        
        return connection.Query<PlayerDTO>(query).ToList();
    }

    public PlayerDTO? GetPlayerById(long playerId)
    {
        using var connection = _connectionProvider.GetConnection();
        const string query = "SELECT * FROM Players WHERE Id = @Id";
        
        return connection.QueryFirstOrDefault<PlayerDTO>(query, new { Id = playerId });
    }

    public int AddPlayer(PlayerDTO player)
    {
        using var connection = _connectionProvider.GetConnection();
        const string query = "INSERT INTO Players (Name, Score) VALUES (@Name, @Score);";
        
        return connection.Execute(query, new { Name = player.Name, Score = player.Score });
    }

    public int UpdatePlayer(PlayerDTO player)
    {
        using var connection = _connectionProvider.GetConnection();

        var query = DatabaseHelpers.BuildUpdateQuery(player);
        var parameters = DatabaseHelpers.PrepareUpdateParameters(player);

        return connection.Execute(query, parameters);
    }
}