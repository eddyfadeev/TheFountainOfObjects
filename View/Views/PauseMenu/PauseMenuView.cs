namespace View.Views.PauseMenu;

public class PauseMenuView : SelectableMenuView<PauseMenuEntries>
{
    public override string MenuName { get; }

    public override ILayoutManager LayoutManager { get; }

    public PauseMenuView(ILayoutManager layoutManager)
    {
        LayoutManager = layoutManager;
        MenuName = "Pause";
    }

    public override PauseMenuEntries Display()
    {
        Console.Clear();
        var pauseMenuEntries = GetEnumValuesAndDisplayNames<PauseMenuEntries>();
        LayoutManager.SupportWindowIsVisible = false;
        
        return SelectEntry(pauseMenuEntries);
    }
}

public enum PauseMenuEntries
{
    [Display(Name = "Resume")]
    Resume,
    [Display(Name = "Help")]
    Help,
    [Display(Name = "Return to main menu")]
    BackToMainMenu,
}