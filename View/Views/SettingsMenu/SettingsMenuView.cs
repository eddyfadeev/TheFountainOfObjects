using Services.GameSettingsRepository;

namespace View.Views.SettingsMenu;

public class SettingsMenuView : SelectableMenuView<SettingsMenuEntries>
{
    public override string MenuName { get; }
    public override ILayoutManager LayoutManager { get; }

    public SettingsMenuView(ILayoutManager layoutManager)
    {
        LayoutManager = layoutManager;
        MenuName = "Settings";
    }

    public override SettingsMenuEntries Display()
    {
        Console.Clear();
        var settingsMenuEntries = GetEnumValuesAndDisplayNames<SettingsMenuEntries>();
        LayoutManager.SupportWindowIsVisible = false;

        return SelectEntry(settingsMenuEntries);
    }

    public static MazeSize AskForMazeSize()
    {
        Console.Clear();
        
        return AnsiConsole.Prompt(GetMazeSizePrompt());
    }

    private static SelectionPrompt<MazeSize> GetMazeSizePrompt() =>
        new SelectionPrompt<MazeSize>()
            .Title("Select maze size")
            .PageSize(10)
            .AddChoices([
                MazeSize.Small,
                MazeSize.Medium,
                MazeSize.Large
            ]);
    
}
