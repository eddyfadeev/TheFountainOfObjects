using System.Text;
using DataObjects.Player;

namespace Services.Database.Helpers;

public static class DatabaseHelpers
{
    public static string BuildUpdateQuery(PlayerDTO player)
    {
        var queryBuilder = new StringBuilder("UPDATE Players SET ");
        const string setName = "Name = @Name";
        const string setScore = "Score = @Score";
        const string idString = " WHERE Id = @Id";

        if (player.Name is not null && player.Score is null)
        {
            queryBuilder.Append($"{setName}");
        }
        else if (player.Name is null && player.Score is not null)
        {
            queryBuilder.Append($"{setScore}");
        }
        else
        {
            queryBuilder.Append($"{setName}, {setScore}");
        }

        queryBuilder.Append(idString);

        return queryBuilder.ToString();
    }

    public static object PrepareUpdateParameters(PlayerDTO player)
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