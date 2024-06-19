using View.Views;

namespace View.SettingsMenu;

public class SettingsMenuView : SelectableMenuView<SettingsMenuEntries>
{
    public override string MenuName => "Settings";
    
    public SettingsMenuEntries ShowSettingsMenu(List<KeyValuePair<SettingsMenuEntries, string>> settingsMenuEntries)
    {
        Console.Clear();
        
        LayoutManager.SupportWindowIsVisible = false;
        
        var selectedEntry = SelectEntry(ref settingsMenuEntries);

        return selectedEntry;
    }
}