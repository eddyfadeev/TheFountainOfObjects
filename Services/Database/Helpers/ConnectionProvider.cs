using Interfaces.Models.Database;
using Microsoft.Data.Sqlite;

namespace Services.Database.Helpers;

public class ConnectionProvider : IConnectionProvider
{
    private const string ConnectionString = "Data Source=players.db";

    public SqliteConnection GetConnection()
    {
        var connection = new SqliteConnection(ConnectionString);
        connection.Open();

        return connection;
    }
}