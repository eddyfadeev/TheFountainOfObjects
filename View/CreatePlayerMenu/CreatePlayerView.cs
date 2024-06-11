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
        Console.CursorVisible = true;
        
        var userName = AnsiConsole.Ask<string>("[white]Please enter your name:[/]");

        Console.CursorVisible = false;
        
        return userName;
    }

    public void ShowAlreadyTakenMessage()
    {
        AnsiConsole.MarkupLine("[red]This name is already taken. Please, choose another one:[/]");
    }
}