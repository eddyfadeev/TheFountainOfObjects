using Model.DataObjects;
using Model.Services;

namespace View.Leaderboard;

public sealed class LeaderboardView : MenuViewBase<Enum>
{
    private static List<PlayerDTO> Players { get; }
    private static readonly DatabaseManager _databaseManager = new();
    
    static LeaderboardView()
    {
        _menuName = "Leaderboard";
        Players = _databaseManager.RetrievePlayers().ToList();
    }
    public void ShowLeaderboard()
    {
        Console.Clear();
        
        var leaderboardTable = CreateLeaderboardTable();
        leaderboardTable.ShowFooters = true;
        
        AnsiConsole.Write(leaderboardTable);
        Console.ReadKey();
    }

    public void ShowTopTen()
    {
        var topTenTable = CreateLeaderboardTable(10);
        topTenTable.ShowFooters = false;
    }

    private Table CreateLeaderboardTable(int? numberOfEntries = null)
    {
        var table = CreateTableLayout();
        var leaderboardTable = CreateInnerTable();
        
        if (numberOfEntries is null || numberOfEntries > Players.Count)
        {
            numberOfEntries = Players.Count;
        }

        for (int i = 0; i < numberOfEntries; i++)
        {
            var player = Players[i];
            leaderboardTable.AddRow(player.Name, player.Score.ToString());
        }

        table.AddRow(leaderboardTable);
        table.Caption = new TableTitle("[white bold]Press any key to continue...[/]");
        
        return table;

        Table CreateInnerTable()
        {
            var innerTable = new Table()
            {
                Border = TableBorder.None,
            };
        
            innerTable.AddColumns("[white bold]Name[/]", "[white bold]Score[/]");
        
            return innerTable;
        }
    }
}