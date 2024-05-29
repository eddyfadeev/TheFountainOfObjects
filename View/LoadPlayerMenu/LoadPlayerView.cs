namespace View.LoadPlayerMenu;

public sealed class LoadPlayerView : MenuViewBase<Enum>
{
    static LoadPlayerView()
    {
        _menuName = "Load Player";
    }

    public static Enum ShowLoadPlayerMenu(IEnumerable<KeyValuePair<Enum, string>> enumEntries)
    {
        _layoutManager.SupportWindowIsVisible = false;
        var selectedEntry = ShowMenu(enumEntries, true);

        return selectedEntry;
    }
}