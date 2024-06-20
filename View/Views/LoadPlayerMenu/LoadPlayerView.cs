using Services.Database.Interfaces;

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

    public override Enum? DisplaySelectable()
    {
        Console.Clear();
        
        LayoutManager.SupportWindowIsVisible = false;
        
        var entries = PrepareData();

        if (entries is null)
        {
            return null;
        }
        
        var selectedEntry = SelectEntry(ref entries);

        return selectedEntry;
    }

    private List<KeyValuePair<Enum, string>>? PrepareData()
    {
        var playerData = _playerRepository.GetAllPlayers();
        const string enumName = "LoadPlayerEnum";
        
        return PrepareEnum(playerData, enumName);
    }
}