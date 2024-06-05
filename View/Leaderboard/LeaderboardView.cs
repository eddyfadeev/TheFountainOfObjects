using Model.DataObjects;
using Model.Services;

namespace View.Leaderboard;

public sealed class LeaderboardView : MenuViewBase<Enum>
{
    private static readonly DatabaseManager _databaseManager = new();
    private List<PlayerDTO> Players { get; } = _databaseManager.RetrievePlayers().ToList();
    public override string MenuName => "Leaderboard";
    
    public void ShowLeaderboard()
    {
        Console.Clear();
        
        var leaderboardTable = CreateLeaderboardTable();
        
        AnsiConsole.Write(leaderboardTable);
        Console.ReadKey();
    }

    public Table CreateTopTen()
    {
        const int numberOfEntries = 10;
        var topTenTable = CreateLeaderboardTable(numberOfEntries);
        topTenTable.ShowFooters = false;

        return topTenTable;
    }

    private Table CreateLeaderboardTable(int? numberOfEntries = null)
    {
        var table = _layoutManager.CreateTableLayout(MenuName);
        var leaderboardTable = CreateInnerTable();

        if (numberOfEntries is null)
        {
            AddCaption(ref table);
        }
        
        if (numberOfEntries is null || numberOfEntries > Players.Count)
        {
            numberOfEntries = Players.Count;
        }

        for (int i = 0; i < numberOfEntries; i++)
        {
            var player = Players[i];
            leaderboardTable.AddRow($"{i + 1} {player.Name}", player.Score.ToString()!);
        }

        table.AddRow(leaderboardTable);
        
        return table;

        Table CreateInnerTable()
        {
            var innerTable = new Table()
            {
                Border = TableBorder.None,
            };
        
            innerTable.AddColumns("[white bold]Name[/]", "[white bold]Score[/]").Centered();
        
            return innerTable;
        }
    }
}