using TheFountainOfObjects.Model.GameObjects;

namespace TheFountainOfObjects.View.CreatePlayerMenu;

public sealed class CreatePlayerView : MenuViewBase<CreatePlayerEntries>
{
    private static readonly IEnumerable<KeyValuePair<CreatePlayerEntries, string>> _createPlayerMenuEntries
        = GetEnumValuesAndDisplayNames<CreatePlayerEntries>();
    
    private static readonly CreatePlayerView Instance = new("Create Player");
    
    private CreatePlayerView(string menuName) : base(menuName)
    {
    }

    public static void ShowCreatePlayerPrompt(Action<CreatePlayerEntries> onMenuEntrySelected)
    {
        _layoutManager.SupportWindowIsVisible = false;
        var selectedEntry = ShowMenu(_createPlayerMenuEntries);
        
        onMenuEntrySelected(selectedEntry);
    }
}