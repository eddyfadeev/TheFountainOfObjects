namespace View.Views.LoadPlayerMenu;

public sealed class LoadPlayerView : SelectableMenuView<Enum>
{
    private readonly IPlayerRepository _playerRepository;
    
    public override string MenuName { get; }
    public override ILayoutManager LayoutManager { get; }
    
    public LoadPlayerView(IPlayerRepository playerRepository, ILayoutManager layoutManager)
    {
        LayoutManager = layoutManager;
        MenuName = "Load Player";
        _playerRepository = playerRepository;
    }

    public override Enum Display()
    {
        Console.Clear();
        
        LayoutManager.SupportWindowIsVisible = false;
        
        var entries = PrepareData();

        if (entries is null)
        {
            AnsiConsole.WriteLine("No players found. Please, create a player first.");
            
            return MenuType.Back;
        }
        
        var selectedEntry = SelectEntry(entries);

        return selectedEntry;
    }

    private List<KeyValuePair<Enum, string>>? PrepareData()
    {
        var playerData = _playerRepository.GetAllPlayers();
        const string enumName = "LoadPlayerEnum";
        
        return PrepareEnum(playerData, enumName);
    }
}