using Dapper;
using Services.Database.Interfaces;

namespace Services.Database.Helpers;

public class DatabaseInitializer(IConnectionProvider connectionProvider) : IDatabaseInitializer
{
    public void InitializeDatabase()
    {
        using var connection = connectionProvider.GetConnection();

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