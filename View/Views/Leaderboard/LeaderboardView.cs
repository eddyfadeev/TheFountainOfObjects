using Interfaces.Models.Player;
using Interfaces.Models.Repository;
using Interfaces.View.LayoutManager;
using Interfaces.View.Menu;
using Interfaces.View.TableBuilder;
using Shared.Enums.Views.Menus;

namespace View.Views.Leaderboard;

public sealed class LeaderboardView : NonSelectableMenuView, ISideMenu<LeaderboardType>
{
    private const int MaxSideEntries = 10;
    
    private readonly ITableBuilderService _tableBuilderService;
    
    private readonly List<IPlayerDTO> _players;
    
    public override string MenuName { get; }

    public LeaderboardView(ILayoutManager layoutManager, IPlayerRepository playerRepository, ITableBuilderService tableBuilderService) : base(layoutManager)
    {
        _tableBuilderService = tableBuilderService;
        
        MenuName = "Leaderboard";
        _players = playerRepository.GetAllPlayers()!;
    }
    
    public override void Display()
    {
        LayoutManager.SupportWindowIsVisible = false;
        
        var leaderboardMenu = CreateLeaderboardTable(LeaderboardType.LeaderboardMenu).Centered();
        
        UpdateLayout(leaderboardMenu);
    }

    public Table GetSideTable(LeaderboardType menuType)
    {
        var sideLeaderboardMenu = CreateLeaderboardTable(LeaderboardType.LeaderboardSideMenu);
        sideLeaderboardMenu.Alignment(Justify.Left);
        
        return sideLeaderboardMenu;
    }

    private Table CreateLeaderboardTable(LeaderboardType leaderboardType)
    {
        var table = _tableBuilderService.CreateOuterTable(MenuName);
        var leaderboardTable = _tableBuilderService.CreateInnerTable();
        
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
            LeaderboardType.LeaderboardSideMenu => Math.Min(_players.Count, MaxSideEntries),
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