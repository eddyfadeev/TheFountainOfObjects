namespace View.LoadPlayerMenu;

public sealed class LoadPlayerView : SelectableMenuViewBase<Enum>
{
    public override string MenuName => "Load Player";

    public Enum? ShowLoadPlayerMenu(List<KeyValuePair<Enum, string>> enumEntries)
    {
        _layoutManager.SupportWindowIsVisible = false;
        
        var selectedEntry = SelectEntry(ref enumEntries);

        return selectedEntry;
    }
}