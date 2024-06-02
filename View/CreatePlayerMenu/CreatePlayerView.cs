namespace View.CreatePlayerMenu;

public sealed class CreatePlayerView : MenuViewBase<CreatePlayerEntries>
{
    private readonly IEnumerable<KeyValuePair<CreatePlayerEntries, string>> _createPlayerMenuEntries
        = GetEnumValuesAndDisplayNames<CreatePlayerEntries>();

    protected override string MenuName => "Create Player";

    public void ShowCreatePlayerPrompt(Action<CreatePlayerEntries> onMenuEntrySelected)
    {
        _layoutManager.SupportWindowIsVisible = false;
        var selectedEntry = ShowMenu(_createPlayerMenuEntries);
        
        onMenuEntrySelected(selectedEntry);
    }
}