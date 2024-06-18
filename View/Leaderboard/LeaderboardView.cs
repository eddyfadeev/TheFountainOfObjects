using Model.Player;
using Services;

namespace View.Leaderboard;

public sealed class LeaderboardView(LeaderboardService leaderboardService) : MenuViewBase
{
    public override string MenuName => "Leaderboard";
    private List<PlayerDTO> _players = leaderboardService.GetPlayers();
    
    public void ShowLeaderboard()
    {
        Console.Clear();
        LayoutManager.SupportWindowIsVisible = false;
        
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
        var table = LayoutManager.CreateTableLayout(MenuName);
        var leaderboardTable = CreateInnerTable();
        
        var numberOfEntriesToAdd = numberOfEntries is null || numberOfEntries > _players.Count ? _players.Count : numberOfEntries.Value;

        if (numberOfEntries is null)
        {
            AddCaption(ref table);
        }
        
        AddPlayersToTable(ref leaderboardTable, numberOfEntriesToAdd);

        table.AddRow(leaderboardTable);
        
        return table;
    }

    private void AddPlayersToTable(ref Table table, int numberOfEntries)
    {
        for (int i = 0; i < numberOfEntries; i++)
        {
            var player = _players[i];
            table.AddRow($"{i + 1} {player.Name}", player.Score.ToString() ?? string.Empty);
        }
    }
    
    private Table CreateInnerTable()
    {
        var innerTable = new Table()
        {
            Border = TableBorder.None,
        };
        
        innerTable.AddColumns("[white bold]Name[/]", "[white bold]Score[/]").Centered();
        
        return innerTable;
    }
}