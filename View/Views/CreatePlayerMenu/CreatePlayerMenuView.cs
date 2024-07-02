using View.Interfaces;

namespace View.Views.CreatePlayerMenu;

public sealed class CreatePlayerMenuView : SelectableMenuView<CreatePlayerEntries>
{
    public override string MenuName { get; }
    public override ILayoutManager LayoutManager { get; }
    private readonly List<KeyValuePair<CreatePlayerEntries, string>> _createPlayerMenuEntries;
    
    public CreatePlayerMenuView(ILayoutManager layoutManager)
    {
        LayoutManager = layoutManager;
        _createPlayerMenuEntries = GetEnumValuesAndDisplayNames<CreatePlayerEntries>();
        MenuName = "Create Player";
    }
    
    public override CreatePlayerEntries Display()
    {
        LayoutManager.SupportWindowIsVisible = false;
        
        return SelectEntry(_createPlayerMenuEntries);
    }
}