using View.Views.SettingsMenu;

namespace View.Commands;

public class ShowSettingsMenuCommand : ICommand
{
    private readonly ILayoutManager _layoutManager;
        
    public ShowSettingsMenuCommand(ILayoutManager layoutManager)
    {
        _layoutManager = layoutManager;
    }
        
    public Enum Execute()
    {
        var settingsMenuView = new SettingsMenuView(_layoutManager);
        return settingsMenuView.Display();
    }
}