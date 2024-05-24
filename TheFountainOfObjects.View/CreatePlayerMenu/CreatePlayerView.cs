namespace TheFountainOfObjects.View.CreatePlayerMenu;

public sealed class CreatePlayerView : MenuViewBase<CreatePlayerEntries>
{
    private static readonly IEnumerable<KeyValuePair<CreatePlayerEntries, string>> _createPlayerMenuEntries
        = GetEnumValuesAndDisplayNames<CreatePlayerEntries>();
    
    private static readonly CreatePlayerView Instance = new();
    private CreatePlayerView() : base("Create Player")
    {
    }

    public static void ShowCreatePlayerPrompt()
    {
        _layoutManager.SupportWindowIsVisible = false;
        var selectedEntry = Instance.ShowMenu(_createPlayerMenuEntries);
        
        InvokeActionForMenuEntry(selectedEntry, Instance);
        Console.ReadKey();
    }
}