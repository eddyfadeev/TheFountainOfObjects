using System.Text;
using Dapper;
using Microsoft.Data.Sqlite;
using TheFountainOfObjects.Model.DataObjects;

namespace TheFountainOfObjects.Model.Services;

public class DatabaseManager
{
    private const string ConnectionString = "Data Source=players.db";
    
    public DatabaseManager()
    {
        InitializeDatabase();
    }

    private SqliteConnection GetConnection()
    {
        var connection = new SqliteConnection(ConnectionString);

        connection.Open();

        return connection;
    }

    private void InitializeDatabase()
    {
        using var connection = GetConnection();

        const string createTableQuery = """
            CREATE TABLE IF NOT EXISTS Players (
                Id INTEGER PRIMARY KEY,
                Name TEXT NOT NULL,
                Score INTEGER NOT NULL
            );
        """;

        connection.Execute(createTableQuery);
    }

    internal int AddPlayer(string name)
    {
        using var connection = GetConnection();

        const string query = """
            INSERT INTO Players (Name, Score)
            VALUES (@Name, 0);
        """;

        return connection.Execute(query, new { Name = name });
    }

    internal IEnumerable<PlayerObject> GetPlayers()
    {
        using var connection = GetConnection();

        const string getPlayersQuery = "SELECT * FROM Players ORDER BY Score DESC";

        return connection.Query<PlayerObject>(getPlayersQuery);
    }

    internal int UpdatePlayer(int playerId, string? name = null, int? score = null)
    {
        using var connection = GetConnection();


        var playerToUpdateQuery = "SELECT * FROM Players WHERE Id = @Id";
        var playerToUpdate = connection.QueryFirstOrDefault<PlayerObject>(playerToUpdateQuery, new { Id = playerId });

        if (playerToUpdate == null)
        {
            throw new ArgumentException("Player not found.");
        }

        var updatePlayerQuery = BuildUpdateQuery(name, score);

        var parameters = PrepareUpdateParameters(playerToUpdate);

        return connection.Execute(updatePlayerQuery, parameters);
    }

    private string BuildUpdateQuery(string? name, int? score)
    {
        var queryBuilder = new StringBuilder("UPDATE Players SET ");
        const string setName = "Name = @Name";
        const string setScore = "Score = @Score";
        const string idString = " WHERE Id = @Id";

        if (name != null && score != null)
        {
            queryBuilder.Append($"{setName}, {setScore}");
        }
        else if (name != null)
        {
            queryBuilder.Append($"{setName}");
        }
        else
        {
            queryBuilder.Append($"{setScore}");
        }

        queryBuilder.Append(idString);

        var finalQuery = queryBuilder.ToString();

        return finalQuery;
    }

    private object PrepareUpdateParameters(PlayerObject player)
    {
        return player switch
        {
            { Name: not null, Score: not null } => new { player.Id, player.Name, player.Score },
            { Name: not null } => new { player.Id, player.Name },
            { Score: not null } => new { player.Id, player.Score },
            _ => throw new ArgumentException("Player must have a name or a score to update.")
        };
    }
}
