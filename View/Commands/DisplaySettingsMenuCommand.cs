using View.Interfaces;
using View.Views.SettingsMenu;

namespace View.Commands;

public class DisplaySettingsMenuCommand : ICommand
{
    private readonly ILayoutManager _layoutManager;
        
    public DisplaySettingsMenuCommand(ILayoutManager layoutManager)
    {
        _layoutManager = layoutManager;
    }
        
    public Enum Execute()
    {
        var settingsMenuView = new SettingsMenuView(_layoutManager);
        return settingsMenuView.DisplaySelectable();
    }
}