using Model.Player;
using Services.Database.Helpers;

namespace Utilities.Tests.Database.Helpers;

public class DatabaseHelpersTests
{
    
    // BuildUpdateQuery should generate correct query when both Name and Score are provided
    [Fact]
    public void build_update_query_generates_correct_query_with_name_and_score()
    {
        var player = new PlayerDTO { Id = 1, Name = "John", Score = 100 };
        var expectedQuery = "UPDATE Players SET Name = @Name, Score = @Score WHERE Id = @Id";
        var actualQuery = DatabaseHelpers.BuildUpdateQuery(player);
        Assert.Equal(expectedQuery, actualQuery);
    }

    // BuildUpdateQuery should generate correct query when only Name is provided
    [Fact]
    public void build_update_query_generates_correct_query_with_only_name()
    {
        var player = new PlayerDTO { Id = 1, Name = "John" };
        var expectedQuery = "UPDATE Players SET Name = @Name WHERE Id = @Id";
        var actualQuery = DatabaseHelpers.BuildUpdateQuery(player);
        Assert.Equal(expectedQuery, actualQuery);
    }

    // BuildUpdateQuery should generate correct query when only Score is provided
    [Fact]
    public void build_update_query_generates_correct_query_with_only_score()
    {
        var player = new PlayerDTO { Id = 1, Score = 100 };
        var expectedQuery = "UPDATE Players SET Score = @Score WHERE Id = @Id";
        var actualQuery = DatabaseHelpers.BuildUpdateQuery(player);
        Assert.Equal(expectedQuery, actualQuery);
    }

    // BuildUpdateQuery should handle null player input gracefully
    [Fact]
    public void build_update_query_handles_null_player_input_gracefully()
    {
        PlayerDTO player = null;
        Assert.Throws<ArgumentNullException>(() => DatabaseHelpers.BuildUpdateQuery(player));
    }

    // PrepareUpdateParameters should throw an exception when both Name and Score are null
    [Fact]
    public void prepare_update_parameters_throws_exception_when_both_name_and_score_are_null()
    {
        var player = new PlayerDTO { Id = 1 };
        Assert.Throws<ArgumentException>(() => DatabaseHelpers.PrepareUpdateParameters(player));
    }

    // BuildUpdateQuery should handle empty string for Name correctly
    [Fact]
    public void build_update_query_handles_empty_string_for_name_correctly()
    {
        var player = new PlayerDTO { Id = 1, Name = "", Score = 100 };
        var expectedQuery = "UPDATE Players SET Name = @Name, Score = @Score WHERE Id = @Id";
        var actualQuery = DatabaseHelpers.BuildUpdateQuery(player);
        Assert.Equal(expectedQuery, actualQuery);
    }

    // Ensure BuildUpdateQuery appends 'WHERE Id = @Id' correctly in all cases
    [Fact]
    public void build_update_query_appends_where_id_correctly_in_all_cases()
    {
        var playerWithNameAndScore = new PlayerDTO { Id = 1, Name = "John", Score = 100 };
        var playerWithNameOnly = new PlayerDTO { Id = 1, Name = "John" };
        var playerWithScoreOnly = new PlayerDTO { Id = 1, Score = 100 };

        var queryWithNameAndScore = DatabaseHelpers.BuildUpdateQuery(playerWithNameAndScore);
        var queryWithNameOnly = DatabaseHelpers.BuildUpdateQuery(playerWithNameOnly);
        var queryWithScoreOnly = DatabaseHelpers.BuildUpdateQuery(playerWithScoreOnly);

        Assert.EndsWith(" WHERE Id = @Id", queryWithNameAndScore);
        Assert.EndsWith(" WHERE Id = @Id", queryWithNameOnly);
        Assert.EndsWith(" WHERE Id = @Id", queryWithScoreOnly);
    }

    // Ensure PrepareUpdateParameters includes Id in all returned objects
    [Fact]
    public void prepare_update_parameters_includes_id_in_all_returned_objects()
    {
        var playerWithNameAndScore = new PlayerDTO { Id = 1, Name = "John", Score = 100 };
        var playerWithNameOnly = new PlayerDTO { Id = 1, Name = "John" };
        var playerWithScoreOnly = new PlayerDTO { Id = 1, Score = 100 };

        var paramsWithNameAndScore = DatabaseHelpers.PrepareUpdateParameters(playerWithNameAndScore);
        var paramsWithNameOnly = DatabaseHelpers.PrepareUpdateParameters(playerWithNameOnly);
        var paramsWithScoreOnly = DatabaseHelpers.PrepareUpdateParameters(playerWithScoreOnly);

        Assert.Contains("Id", paramsWithNameAndScore.GetType().GetProperties().Select(p => p.Name));
        Assert.Contains("Id", paramsWithNameOnly.GetType().GetProperties().Select(p => p.Name));
        Assert.Contains("Id", paramsWithScoreOnly.GetType().GetProperties().Select(p => p.Name));
    }


}