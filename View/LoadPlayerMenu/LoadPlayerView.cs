using Model.Services;

namespace View.LoadPlayerMenu;

public sealed class LoadPlayerView : MenuViewBase<Enum>
{
    static LoadPlayerView()
    {
        _menuName = "Load Player";
    }

    public static Enum ShowLoadPlayerMenu(IEnumerable<KeyValuePair<Enum, string>> enumEntries, Action<Enum> onMenuEntrySelected)
    {
        _layoutManager.SupportWindowIsVisible = false;
        var selectedEntry = ShowMenu(enumEntries);

        return selectedEntry;
    }
}