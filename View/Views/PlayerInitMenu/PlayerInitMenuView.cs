using Interfaces.View.LayoutManager;
using Shared.Enums.Views.Menus;

namespace View.Views.PlayerInitMenu;

public sealed class PlayerInitMenuView : SelectableMenuView<PLayerInitMenuEntries>
{
    public override string MenuName { get; }
    public override ILayoutManager LayoutManager { get; }
    private readonly List<KeyValuePair<PLayerInitMenuEntries, string>> _createPlayerMenuEntries;
    
    public PlayerInitMenuView(ILayoutManager layoutManager)
    {
        LayoutManager = layoutManager;
        _createPlayerMenuEntries = GetEnumValuesAndDisplayNames<PLayerInitMenuEntries>();
        
        MenuName = "Create Player";
    }
    
    public override PLayerInitMenuEntries Display()
    {
        LayoutManager.SupportWindowIsVisible = false;
        
        return SelectEntry(_createPlayerMenuEntries);
    }
}