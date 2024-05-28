namespace View.CreatePlayerMenu;

public sealed class CreatePlayerView : MenuViewBase<CreatePlayerEntries>
{
    private static readonly IEnumerable<KeyValuePair<CreatePlayerEntries, string>> _createPlayerMenuEntries
        = GetEnumValuesAndDisplayNames<CreatePlayerEntries>();

    static CreatePlayerView()
    {
        _menuName = "Create Player";
    }

    public static void ShowCreatePlayerPrompt(Action<CreatePlayerEntries> onMenuEntrySelected)
    {
        _layoutManager.SupportWindowIsVisible = false;
        var selectedEntry = ShowMenu(_createPlayerMenuEntries);
        
        onMenuEntrySelected(selectedEntry);
    }
}