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

    public override SettingsMenuEntries DisplaySelectable()
    {
        Console.Clear();
        var settingsMenuEntries = GetEnumValuesAndDisplayNames<SettingsMenuEntries>();
        LayoutManager.SupportWindowIsVisible = false;

        return SelectEntry(ref settingsMenuEntries);
    }
}