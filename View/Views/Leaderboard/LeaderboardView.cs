using Model.Player;

namespace View.Views.Leaderboard;

public sealed class LeaderboardView : MenuView
{
    private readonly List<PlayerDTO> _players;
    
    public override string MenuName { get; }
    public override ILayoutManager LayoutManager { get; }

    public LeaderboardView(IPlayerRepository playerRepository, ILayoutManager layoutManager)
    {
        LayoutManager = layoutManager;
        MenuName = "Leaderboard";
        _players = playerRepository.GetAllPlayers();
    }
    
    public override void Display()
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
            AddCaption(table);
        }
        
        AddPlayersToTable(leaderboardTable, numberOfEntriesToAdd);

        table.AddRow(leaderboardTable);
        
        return table;
    }

    private void AddPlayersToTable(Table table, int numberOfEntries)
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