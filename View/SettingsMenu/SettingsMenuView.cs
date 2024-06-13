namespace View.SettingsMenu;

public class SettingsMenuView : SelectableMenuViewBase<SettingsMenuEntries>
{
    public override string MenuName => "Settings";
    
    public SettingsMenuEntries ShowSettingsMenu(List<KeyValuePair<SettingsMenuEntries, string>> settingsMenuEntries)
    {
        Console.Clear();
        
        _layoutManager.SupportWindowIsVisible = false;
        
        var selectedEntry = SelectEntry(ref settingsMenuEntries);

        return selectedEntry;
    }
}