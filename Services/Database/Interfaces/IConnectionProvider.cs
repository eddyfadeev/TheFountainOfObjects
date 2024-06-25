using Microsoft.Data.Sqlite;

namespace Services.Database.Interfaces;

public interface IConnectionProvider
{
    SqliteConnection GetConnection();
}