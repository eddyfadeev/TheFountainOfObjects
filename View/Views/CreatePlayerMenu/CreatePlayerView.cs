using View.Interfaces;

namespace View.Views.CreatePlayerMenu;

public sealed class CreatePlayerView : SelectableMenuView<CreatePlayerEntries>
{
    public override string MenuName { get; }
    public override ILayoutManager LayoutManager { get; }
    private List<KeyValuePair<CreatePlayerEntries, string>> _createPlayerMenuEntries;
    
    public CreatePlayerView(ILayoutManager layoutManager)
    {
        LayoutManager = layoutManager;
        _createPlayerMenuEntries = GetEnumValuesAndDisplayNames<CreatePlayerEntries>();
        MenuName = "Create Player";
    }
    
    public override CreatePlayerEntries Display()
    {
        LayoutManager.SupportWindowIsVisible = false;
        
        return SelectEntry(ref _createPlayerMenuEntries);
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