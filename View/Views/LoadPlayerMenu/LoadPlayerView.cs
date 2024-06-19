using View.Views;

namespace View.LoadPlayerMenu;

public sealed class LoadPlayerView : SelectableMenuView<Enum>
{
    public override string MenuName => "Load Player";

    public Enum? ShowLoadPlayerMenu(List<KeyValuePair<Enum, string>> enumEntries)
    {
        Console.Clear();
        
        LayoutManager.SupportWindowIsVisible = false;
        
        var selectedEntry = SelectEntry(ref enumEntries);

        return selectedEntry;
    }
}