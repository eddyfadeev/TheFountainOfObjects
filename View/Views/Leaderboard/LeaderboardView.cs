using Interfaces.Models.Database;
using Interfaces.Models.Player;
using Interfaces.View.LayoutManager;
using Interfaces.View.Menu;
using Model.Player;
using Shared.Enums.Views.Menus;

namespace View.Views.Leaderboard;

public sealed class LeaderboardView : MenuView, ISideMenu<LeaderboardType>
{
    private readonly List<IPlayerDTO> _players;
    
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
        LayoutManager.SupportWindowIsVisible = false;
        
        var leaderboardMenu = CreateLeaderboardTable(LeaderboardType.LeaderboardMenu).Centered();

        LayoutManager.MainWindow.Update(leaderboardMenu);
        LayoutManager.UpdateLayout();
        Console.ReadKey();
    }

    public Table GetSideTable(LeaderboardType menuType)
    {
        var sideLeaderboardMenu = CreateLeaderboardTable(LeaderboardType.LeaderboardSideMenu);
        sideLeaderboardMenu.Alignment(Justify.Left);
        
        return sideLeaderboardMenu;
    }

    private Table CreateLeaderboardTable(LeaderboardType leaderboardType)
    {
        var table = CreateOuterTable(MenuName);
        var leaderboardTable = CreateInnerTable();
        
        leaderboardTable.AddColumns("[white bold]Name[/]", "[white bold]Score[/]");
        

        if (leaderboardType is LeaderboardType.LeaderboardMenu)
        {
            AddCaption(table);
            leaderboardTable.Centered();
        }
        
        var numberOfEntriesToAdd = CalculateNumberOfEntries(leaderboardType);
        
        AddPlayersToTable(leaderboardTable, numberOfEntriesToAdd);

        table.AddRow(leaderboardTable);
        
        return table;
    }

    private int CalculateNumberOfEntries(LeaderboardType leaderboardType) =>
        leaderboardType switch
        {
            LeaderboardType.LeaderboardMenu => _players.Count,
            LeaderboardType.LeaderboardSideMenu => _players.Count <= 10? _players.Count : 10,
            _ => throw new ArgumentException("Wrong leaderboard type")
        };
    

    private void AddPlayersToTable(Table table, int numberOfEntries)
    {
        for (int i = 0; i < numberOfEntries; i++)
        {
            var player = _players[i];
            table.AddRow($"{i + 1} {player.Name}", player.Score.ToString() ?? string.Empty);
        }
    }
}