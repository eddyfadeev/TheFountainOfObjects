using Dapper;
using Services.Database.Interfaces;

namespace Services.Database.Helpers;

public class DatabaseInitializer : IDatabaseInitializer
{
    private readonly IConnectionProvider _connectionProvider;
    
    public DatabaseInitializer(IConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }
    public void InitializeDatabase()
    {
        using var connection = _connectionProvider.GetConnection();

        const string createTableQuery = 
            """
                CREATE TABLE IF NOT EXISTS Players (
                Id INTEGER PRIMARY KEY,
                Name TEXT NOT NULL,
                Score INTEGER NOT NULL
                );
            """;

        connection.Execute(createTableQuery);
    }
}