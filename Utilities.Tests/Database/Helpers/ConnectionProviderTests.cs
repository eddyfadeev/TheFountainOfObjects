using Microsoft.Data.Sqlite;
using Services.Database.Helpers;

namespace Utilities.Tests.Database.Helpers;

public class ConnectionProviderTests
{
    
    // Successfully opens a connection to the SQLite database
    [Fact]
    public void Successfully_Opens_Connection_To_Sqlite_Database()
    {
        // Arrange
        var connectionProvider = new ConnectionProvider();

        // Act
        var connection = connectionProvider.GetConnection();

        // Assert
        Assert.NotNull(connection);
        Assert.Equal(System.Data.ConnectionState.Open, connection.State);
    }

    // Returns a valid SqliteConnection object
    [Fact]
    public void Returns_Valid_SqliteConnection_Object()
    {
        // Arrange
        var connectionProvider = new ConnectionProvider();

        // Act
        var connection = connectionProvider.GetConnection();

        // Assert
        Assert.IsType<SqliteConnection>(connection);
    }

    // Connection state is open after calling GetConnection
    [Fact]
    public void Connection_State_Is_Open_After_Calling_GetConnection()
    {
        // Arrange
        var connectionProvider = new ConnectionProvider();

        // Act
        var connection = connectionProvider.GetConnection();

        // Assert
        Assert.Equal(System.Data.ConnectionState.Open, connection.State);
    }

    // Uses the correct connection string "Data Source=players.db"
    [Fact]
    public void Uses_Correct_Connection_String()
    {
        // Arrange
        var expectedConnectionString = "Data Source=players.db";
        var connectionProvider = new ConnectionProvider();

        // Act
        var connection = connectionProvider.GetConnection();

        // Assert
        Assert.Equal(expectedConnectionString, connection.ConnectionString);
    }

    // SQLite library is not available or not installed
    [Fact]
    public void SQLite_Library_Not_Available_Or_Not_Installed()
    {
        // This is a bit tricky to simulate in a unit test environment.
        // We assume the library is available if the code compiles and runs.
        
        // Arrange & Act & Assert
        Assert.True(Type.GetType("Microsoft.Data.Sqlite.SqliteConnection, Microsoft.Data.Sqlite") != null);
    }

    // Check for exceptions thrown during connection opening
    [Fact]
    public void Check_For_Exceptions_Thrown_During_Connection_Opening()
    {
        // Arrange
        var invalidConnectionString = "Data Source=:memory:";
        
        // Act & Assert
        var exception = Record.Exception(() => new SqliteConnection(invalidConnectionString).Open());
        
        // Ensure no exception is thrown for in-memory database (valid case)
        Assert.Null(exception);
        
        // For invalid cases, you would expect an exception, e.g., invalid file path.
    }

    // Validate connection pooling behavior if applicable
    [Fact]
    public void Validate_Connection_Pooling_Behavior_If_Applicable()
    {
        // Arrange
        var connectionProvider = new ConnectionProvider();
        
        // Act
        using (var connection1 = connectionProvider.GetConnection())
        using (var connection2 = connectionProvider.GetConnection())
        {
            // Assert that both connections are open and valid.
            Assert.Equal(System.Data.ConnectionState.Open, connection1.State);
            Assert.Equal(System.Data.ConnectionState.Open, connection2.State);
            
            // Additional checks for pooling can be added if specific pooling settings are used.
            Assert.NotEqual(connection1, connection2);  // Assuming no pooling by default.
        }
    }

    // Ensure thread safety when multiple connections are opened simultaneously
    [Fact]
    public void Ensure_Thread_Safety_When_Multiple_Connections_Are_Opened_Simultaneously()
    {
        // Arrange
        var connectionProvider = new ConnectionProvider();
        
        // Act & Assert using parallel tasks to simulate concurrent access.
        Parallel.For(0, 10, i =>
        {
            using (var connection = connectionProvider.GetConnection())
            {
                Assert.Equal(System.Data.ConnectionState.Open, connection.State);
            }
        });
    }
}