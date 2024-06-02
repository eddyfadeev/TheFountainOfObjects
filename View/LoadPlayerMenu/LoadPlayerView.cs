namespace View.LoadPlayerMenu;

public sealed class LoadPlayerView : MenuViewBase<Enum>
{
    protected override string MenuName => "Load Player";

    public  Enum ShowLoadPlayerMenu(IEnumerable<KeyValuePair<Enum, string>> enumEntries)
    {
        _layoutManager.SupportWindowIsVisible = false;
        var selectedEntry = ShowMenu(enumEntries, true);

        return selectedEntry;
    }
}