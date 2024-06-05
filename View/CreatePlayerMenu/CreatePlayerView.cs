using static Utilities.UserInputService;
using Model.GameObjects;

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

    // TODO: Finish this guy
    public static Player CreatePlayer()
    {
        Console.Clear();
        
        var existentName = true;

        while (existentName)
        {
            AnsiConsole.MarkupLine("[white]Please enter your name (Press ESC to cancel):[/]");
            
            var userInput = AskForInput();
        }

        try
        {
            Console.CursorVisible = true;
        }
        finally
        {
            Console.CursorVisible = false;
        }
        var userName = Console.ReadLine();

        return new Player("S", 0);
    }
}