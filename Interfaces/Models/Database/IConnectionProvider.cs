using Microsoft.Data.Sqlite;

namespace Interfaces.Models.Database;

public interface IConnectionProvider
{
    SqliteConnection GetConnection();
}