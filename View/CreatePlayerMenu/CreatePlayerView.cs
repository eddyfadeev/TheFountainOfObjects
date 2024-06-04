namespace View.CreatePlayerMenu;

public sealed class CreatePlayerView : SelectableMenuViewBase<CreatePlayerEntries>
{
    private List<KeyValuePair<CreatePlayerEntries, string>> _createPlayerMenuEntries
        = GetEnumValuesAndDisplayNames<CreatePlayerEntries>();
    public override string MenuName => "Create Player";

    public void ShowCreatePlayerPrompt(Action<CreatePlayerEntries> onMenuEntrySelected)
    {
        _layoutManager.SupportWindowIsVisible = false;
        
        var selectedEntry = SelectEntry(ref _createPlayerMenuEntries);
        
        onMenuEntrySelected(selectedEntry);
    }
}