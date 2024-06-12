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

    public string AskForUserName()
    {
        const string message = "Please enter your name:";
        var userName = GetUserInput(ChangeStringColor(message, Color.White));
        
        return userName;
    }

    public void ShowAlreadyTakenMessage()
    {
        const string message = "This name is already taken. Please, choose another one:";
        
        AnsiConsole.MarkupLine(ChangeStringColor(message, Color.Red));
    }
}