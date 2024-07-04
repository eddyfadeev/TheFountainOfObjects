namespace View.Views.GameStats;

public class GameStatsView : ISideMenu<GameStatsType>
{
    private readonly string _menuName;
    private readonly ILayoutManager _layoutManager;
    private readonly IPlayerRepository _playerRepository;

    public GameStatsView(ILayoutManager layoutManager, IPlayerRepository playerRepository)
    {
        _layoutManager = layoutManager;
        _playerRepository = playerRepository;
        _menuName = "Stats and Messages";
    }

    public Table GetSideTable(GameStatsType menuType)
    {
        var sideStats = CreateStatsTable();

        return sideStats;
    }

    private Table CreateStatsTable()
    {
        var table = _layoutManager.CreateTableLayout(_menuName);
        var statsTable = PrepareStats();
        
        table.AddRow(statsTable);

        return table;
    }

    private Table PrepareStats()
    {
        var statsTable = _layoutManager.CreateInnerTable();
        statsTable.AddColumns("Entry", "Value");
        
        AddStats(statsTable);

        return statsTable;
    }

    private void AddStats(Table statsTable)
    {
        if (_playerRepository.Player is null)
        {
            throw new ArgumentException("Player is null in Game Stats View.");
        }
        
        var playerName = _playerRepository.Player.Name;
        var playerArrows = _playerRepository.Player.Arrows;
        var currentLocation = _playerRepository.Player.Location;

        statsTable.AddRow("[white]Name[/]", playerName!);
        statsTable.AddRow("[white]Arrows[/]", playerArrows.ToString());
        statsTable.AddRow("[white]Location[/]", currentLocation.ToString()!);
    }
}